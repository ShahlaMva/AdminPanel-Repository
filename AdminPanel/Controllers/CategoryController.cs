using AdminPanel.Db;
using AdminPanel.Models;
using AdminPanel.Services.CategoryServ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult>Detail(int id)
        {if (id == null) return NotFound();
          Category category  = await _categoryService.GetCategoryByIdAsync(id);
            if(category == null) return NotFound();
            return View(category);
        }

        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)

        { if (category == null) return NotFound();
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }

            Category ctgnew = new Category {
            Name=category.Name,
            CreateTime=DateTime.Now,
            SoftDelete=false,
            
            };
          await _categoryService.AddCategoryAsync(ctgnew);
            
            return RedirectToAction("Index", "Category");

        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id==null)return NotFound();
            Category category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            await _categoryService.DeleteCategoryAsync(id);
           category.SoftDelete= true;
            
            return RedirectToAction("Index","Category");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {if(id==null) return NotFound();
            Category category = await _categoryService.GetCategoryByIdAsync(id);
            if(category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            Category? dbctg= await _categoryService.GetCategoryByIdAsync(id);
            if(dbctg == null)return NotFound();
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid)
            {
                return View(dbctg);
            }
            dbctg.Name= category.Name;
            dbctg.UpdateTime=DateTime.Now;
           await _categoryService.UpdateCategoryAsync(dbctg);  

            return RedirectToAction("Index", "Category");
        }
    }
}
