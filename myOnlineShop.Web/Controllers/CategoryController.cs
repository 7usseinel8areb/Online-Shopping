using Microsoft.AspNetCore.Mvc;

namespace myOnlineShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
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
                _context.Categories.Add(newCategory);
                _context.SaveChanges();
                TempData["Create"] = "Item was added succesfully";
                return RedirectToAction("Index");
            }
            return View(newCategory);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int? id)
        {
            var category = _context.Categories.Find(id);
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
                _context.Categories.Update(updatedCategory);
                _context.SaveChanges();
                TempData["Update"] = "Item updated succesfully";
                return RedirectToAction("Index");
            }
            return View(updatedCategory);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null)
                NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["Delete"] = "Item has been Deleted Succesfully";
            return RedirectToAction("Index");
        }
    }
}
