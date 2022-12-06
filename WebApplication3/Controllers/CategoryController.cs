using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _service.GetAllCategories();
            return View("Index",objCategoryList);
            
        }
        
        public IActionResult Create()
        {
            return View("Create");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _service.AddCategory(obj);
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        
        public IActionResult Edit( int id)
        {

           var categoryFromDb = _service.FindCategory(id);
           return View("Edit",categoryFromDb);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _service.EditCategory(obj);
                return RedirectToAction("Index");
            }

            return View("Edit",obj);
        }
        
        public IActionResult Delete(int id)
        {
            var categoryDb = _service.FindCategory(id);
            return View("Delete",categoryDb);
        }
        
        [HttpPost]
        public IActionResult DeletePost(Category obj)
        {
            _service.DeleteCategory(obj);
            return RedirectToAction("Index");
        }
    }
}