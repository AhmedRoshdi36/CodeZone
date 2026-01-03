using CodeZone.BLL.Services.Implementations;
using CodeZone.BLL.Services.Interfaces;
using CodeZone.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeZone.MVC.Controllers
{
    public class StockTransactionController(
        IStockService stockService,
        IWarehouseService warehouseService,
        IProductService productService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await stockService.GetAllAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var transaction = await stockService.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("WarehouseId,ProductId,Quantity")] StockTransaction stockTransaction)
        {
            if (ModelState.IsValid)
            {
                var result = await stockService.CreateAsync(stockTransaction);

                if (result.IsFailure)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    await LoadDropdownsAsync();
                    return View(stockTransaction);
                }

                return RedirectToAction(nameof(Index));
            }
            await LoadDropdownsAsync();
            return View(stockTransaction);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await stockService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();

            ViewBag.Warehouses = await warehouseService.GetAllAsync();
            ViewBag.Products = await productService.GetAllAsync();
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  StockTransaction stockTransaction)
        {
            if (id != stockTransaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await stockService.UpdateAsync(stockTransaction);

                if (result.IsFailure)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    await LoadDropdownsAsync();
                    return View(stockTransaction);
                }

                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownsAsync();
            return View(stockTransaction);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await stockService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();
                
            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await stockService.DeleteAsync(id);

            if (result.IsFailure)
            {
                TempData["Error"] = result.ErrorMessage;
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }

      
        [HttpGet]
        public async Task<IActionResult> GetCurrentStock(int warehouseId, int productId)
        {
            var stock = await stockService.GetCurrentStockAsync(warehouseId, productId);
            return Json(stock);
        }
        private async Task LoadDropdownsAsync()
        {
            ViewBag.Warehouses = await warehouseService.GetAllAsync();
            ViewBag.Products = await productService.GetAllAsync();
        }


    }
}

