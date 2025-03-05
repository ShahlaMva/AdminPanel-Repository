using AdminPanel.Helpers.Extensions;
using AdminPanel.Helpers.Generics;
using AdminPanel.Models;
using AdminPanel.Services.CategoryServ;
using AdminPanel.Services.ProductServ;
using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private IWebHostEnvironment _webHostEnvironment;


        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {


            var products = await _productService.GetPaginateAsync(page, 4);
            var categories = await _categoryService.GetAllCategoriesAsync();

            ProductCategoryVM proVm = new ProductCategoryVM
            {
                Products=products,
                Categories=categories
            };
           int totalPage = await ProductsCount(4);
            
            Pagination<ProductCategoryVM> pagination = new(proVm, totalPage, page);
                
         
            return View(pagination);
        }
        private async Task<int> ProductsCount(int take)
        {
            int proCount = await _productService.ProductCountAsync();

            return (int)Math.Ceiling((decimal)proCount / take);

        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Category=await _categoryService.GetAllCategoriesAsync();

           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product,int CategoryId)
        {
            if (product == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
             
                var newPro = new Product()
                {

                    Name = product.Name,
                    Description = product.Description,
                    Count = product.Count,
                   SalePrice = product.SalePrice,
                    Price = product.Price,
                    DisCount = product.DisCount,
                    CreateTime= DateTime.Now,
                   CategoryId= CategoryId
                    
                };

                if (!product.ProductImage.IsPhoto())
                {
                    ModelState.AddModelError("ProductImage", "Is not image type");
                }
                if (product.ProductImage.PhotoSize(3))
                {
                    ModelState.AddModelError("ProductImage", "Image size is large");
                }

                string fileName = await product.ProductImage.CopyPhoto(_webHostEnvironment.WebRootPath,"img");
                newPro.ProductPhoto = fileName;
                await _productService.AddProductAsync(newPro);
                return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Detail(int id)
        {
            if(id==null) return NotFound();
            ProductCategoryVM pro = new ProductCategoryVM()
            {
                Categories = await _categoryService.GetAllCategoriesAsync(),
                Product = await _productService.GetProductByIdAsync(id),
            };
            if(pro==null) return NotFound();
            
            return View(pro);
        }
        public async Task<IActionResult>Delete(int id)
        {
            if(id==null) return NotFound();
         
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            Product product = await _productService.GetProductByIdAsync(id);
            if(product==null) return NotFound();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Product product)
        {
 
            Product dbpro=await _productService.GetProductByIdAsync(id);
            if(dbpro==null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            dbpro.Name = product.Name;
            dbpro.Description = product.Description;    
            dbpro.Count=product.Count;
            dbpro.SalePrice= product.SalePrice;
            dbpro.Price= product.Price;
            dbpro.DisCount= product.DisCount;
            dbpro.UpdateTime=DateTime.Now;

            await _productService.UpdateProductAsync(dbpro);

            return RedirectToAction("Index");

           
            
        }

       
    }
}