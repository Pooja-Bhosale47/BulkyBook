using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _CategoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _CategoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot Exactly Match the Name");
            }
            if (ModelState.IsValid)
            {
                _CategoryRepo.Add(obj);
                _CategoryRepo.save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoryfromdb = _CategoryRepo.Get(u=>u.Id==Id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _CategoryRepo.update(obj);
                _CategoryRepo.save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoryfromdb = _CategoryRepo.Get(u=>u.Id==Id);
            if (categoryfromdb == null)
            {
                return NotFound();
            }
            return View(categoryfromdb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? obj = _CategoryRepo.Get(u=>u.Id==Id);
            if(obj==null)
            {
                return NotFound();
            }

            _CategoryRepo.Remove(obj);
            _CategoryRepo.save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
            
        }
    }
}
