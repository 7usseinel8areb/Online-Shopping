using Microsoft.AspNetCore.Mvc;
using myOnlineShop.DataAccess.Data;
using myOnlineShop.Enities.Repositories;
using myOnlineShop.Entities.Models;

namespace myOnlineShop.DataAccess.Controllers
{
    public class CategoryController : Controller
    {
        public IUnitOfwork _unitOfwork;

        public CategoryController(IUnitOfwork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfwork.Category.GetAll().ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken] // Secure from hacking => Cross side forgery attack
        public IActionResult Create([FromForm] Category newCategory)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Category.Add(newCategory);
                _unitOfwork.Complete();
                TempData["Create"] = "Item was added succesfully";
                return RedirectToAction("Index");
            }
            return View(newCategory);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int? id)
        {
            var category =_unitOfwork.Category.GetFirstOrDefault(c => c.Id == id);
            if (category == null || id == null)
                return NotFound("This category wasn't found");
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(/*[FromForm] int id,*/ Category updatedCategory)
        {
            if (ModelState.IsValid)
            {
                /*Category oldCategory = _context.Categories.Find(id);
                if (oldCategory == null)
                    return NotFound();
                oldCategory.Name = updatedCategory.Name;
                oldCategory.Description = updatedCategory.Description;
                _context.SaveChanges();*/
                /*_context.Categories.Update(updatedCategory);
                _context.SaveChanges();*/

                _unitOfwork.Category.Update(updatedCategory);
                _unitOfwork.Complete();
                TempData["Update"] = "Item updated succesfully";
                return RedirectToAction("Index");
            }
            return View(updatedCategory);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            Category category = _unitOfwork.Category.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
                NotFound();
            _unitOfwork.Category.Delete(category);
            _unitOfwork.Complete();

            TempData["Delete"] = "Item has been Deleted Succesfully";
            return RedirectToAction("Index");
        }
    }
}
