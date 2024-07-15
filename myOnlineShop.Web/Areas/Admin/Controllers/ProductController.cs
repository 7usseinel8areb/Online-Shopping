using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myOnlineShop.DataAccess.Data;
using myOnlineShop.Enities.Repositories;
using myOnlineShop.Entities.Models;
using myOnlineShop.Entities.ViewModels;

namespace myOnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfwork _unitOfwork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfwork unitOfwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfwork = unitOfwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfwork.Product.GetAll().ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfwork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }),
            };
            
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Secure from hacking => Cross side forgery attack
        public IActionResult Create([FromForm] ProductVM newProduct,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;//wwwRoot
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();//fake name
                    var Upload = Path.Combine(RootPath, @"Images\Products");//will add image here
                    var imgExtention = Path.GetFileName(file.FileName);

                    using(var fileStream = new FileStream(Path.Combine(Upload, fileName+imgExtention),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    newProduct.Product.Image = @"Images/Products/"+fileName+imgExtention;
                }
                _unitOfwork.Product.Add(newProduct.Product);
                _unitOfwork.Complete();
                TempData["Create"] = "Item was added succesfully";
                return RedirectToAction("Index");
            }
            ProductVM productVM = new ProductVM()
            {
                Product = newProduct.Product,
                CategoryList = _unitOfwork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }),
            };
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            var Product = _unitOfwork.Product.GetFirstOrDefault(c => c.Id == id);
            if (Product == null || id == null)
                return NotFound("This Product wasn't found");
            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(/*[FromForm] int id,*/ Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Product.Update(updatedProduct);
                _unitOfwork.Complete();
                TempData["Update"] = "Item updated succesfully";
                return RedirectToAction("Index");
            }
            return View(updatedProduct);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            Product product = _unitOfwork.Product.GetFirstOrDefault(c => c.Id == id);
            if (product == null)
                NotFound();
            _unitOfwork.Product.Delete(product);
            _unitOfwork.Complete();

            TempData["Delete"] = "Item has been Deleted Succesfully";
            return RedirectToAction("Index");
        }
    }
}
