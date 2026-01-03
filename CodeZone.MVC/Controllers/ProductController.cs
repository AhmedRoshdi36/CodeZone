using CodeZone.BLL.Services.Interfaces;
using CodeZone.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeZone.MVC.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var result = await productService.GetAllPaginatedAsync(page, pageSize);
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,SKU,Description")] Product product)
        {
            if (ModelState.IsValid)
            { 
                var result = await productService.CreateAsync(product);
                if (result.IsFailure)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(product);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await productService.UpdateAsync(product);

                if (result.IsFailure)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(product);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await productService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData["Error"] = result.ErrorMessage;
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

