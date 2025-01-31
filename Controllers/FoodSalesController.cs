using Food_Sale.Models;
using Food_Sale.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Food_Sale.Controllers
{
    public class FoodSalesController : Controller
    {
        private readonly ExcelService _excelService;

        public FoodSalesController(ExcelService excelService)
        {
            _excelService = excelService;
        }


        public IActionResult Index()
        {
            var data = _excelService.GetAllData();
            return View(data); 
        }


        [HttpPost]
        public IActionResult Add(FoodSale newSale)
        {
            if (ModelState.IsValid)
            {
                var data = _excelService.GetAllData();
                data.Add(newSale);
                _excelService.SaveData(data);
                return RedirectToAction("Index"); 
            }
            return View(newSale);
        }


        [HttpPost]
        public IActionResult Edit(FoodSale updatedSale)
        {
            if (ModelState.IsValid)
            {
                var data = _excelService.GetAllData();
                var sale = data.FirstOrDefault(x => x.OrderDate == updatedSale.OrderDate && x.Region == updatedSale.Region);

                if (sale == null)
                {
                    return NotFound("ไม่พบข้อมูลที่ต้องการแก้ไข");
                }

                sale.City = updatedSale.City;
                sale.Category = updatedSale.Category;
                sale.Product = updatedSale.Product;
                sale.Quantity = updatedSale.Quantity;
                sale.UnitPrice = updatedSale.UnitPrice;
                sale.TotalPrice = updatedSale.TotalPrice;

                _excelService.SaveData(data);
                return RedirectToAction("Index"); 
            }
            return View(updatedSale); 
        }

        [HttpPost]
        public IActionResult Delete(string orderDate, string region)
        {
            var data = _excelService.GetAllData();
            var sale = data.FirstOrDefault(x => x.OrderDate.ToString("yyyy-MM-dd") == orderDate && x.Region == region);

            if (sale == null)
            {
                return NotFound("ไม่พบข้อมูลที่ต้องการลบ");
            }

            data.Remove(sale);
            _excelService.SaveData(data);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(string orderDate, string region)
        {
            var data = _excelService.GetAllData();
            var sale = data.FirstOrDefault(x => x.OrderDate.ToString("yyyy-MM-dd") == orderDate && x.Region == region);

            if (sale == null)
            {
                return NotFound("ไม่พบข้อมูลที่ต้องการแก้ไข");
            }

            return View(sale);
        }
    }
}
