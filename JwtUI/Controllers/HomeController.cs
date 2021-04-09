using System.Threading.Tasks;
using JwtUI.APIServices.Interfaces;
using JwtUI.CustomFilters;
using JwtUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [JwtAuth("Admin")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(ProductAdd model)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(model);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Problem ile karşılaştık");
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var editModel = new ProductEdit() { Name = product.Name, Id = product.Id };

            return View(editModel);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductEdit model)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(model);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Problem ile karşılaştık");
            return View();
        }
    }
}