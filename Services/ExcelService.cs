using Food_Sale.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Food_Sale.Services
{
    public class ExcelService
    {
        private readonly string _filePath = "wwwroot/Data/Food Sales.xlsx";

        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public List<FoodSale> GetAllData()
        {
            using var package = new ExcelPackage(new FileInfo(_filePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null) return [];

            int rowCount = worksheet.Dimension.Rows;
            var foodSales = new List<FoodSale>();

            for (int row = 2; row <= rowCount; row++)
            {
                foodSales.Add(new FoodSale
                {
                    OrderDate = worksheet.Cells[row, 1].GetValue<DateTime>(),
                    Region = worksheet.Cells[row, 2].GetValue<string>(),
                    City = worksheet.Cells[row, 3].GetValue<string>(),
                    Category = worksheet.Cells[row, 4].GetValue<string>(),
                    Product = worksheet.Cells[row, 5].GetValue<string>(),
                    Quantity = worksheet.Cells[row, 6].GetValue<int>(),
                    UnitPrice = worksheet.Cells[row, 7].GetValue<decimal>(),
                    TotalPrice = worksheet.Cells[row, 8].GetValue<decimal>()
                });
            }

            return foodSales;
        }

        public void SaveData(List<FoodSale> data)
        {
            FileInfo file = new FileInfo(_filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells.Clear();

                worksheet.Cells[1, 1].Value = "OrderDate";
                worksheet.Cells[1, 2].Value = "Region";
                worksheet.Cells[1, 3].Value = "City";
                worksheet.Cells[1, 4].Value = "Category";
                worksheet.Cells[1, 5].Value = "Product";
                worksheet.Cells[1, 6].Value = "Quantity";
                worksheet.Cells[1, 7].Value = "UnitPrice";
                worksheet.Cells[1, 8].Value = "TotalPrice";

                int rowIndex = 2;
                foreach (var sale in data)
                {
                    worksheet.Cells[rowIndex, 1].Value = sale.OrderDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[rowIndex, 2].Value = sale.Region;
                    worksheet.Cells[rowIndex, 3].Value = sale.City;
                    worksheet.Cells[rowIndex, 4].Value = sale.Category;
                    worksheet.Cells[rowIndex, 5].Value = sale.Product;
                    worksheet.Cells[rowIndex, 6].Value = sale.Quantity;
                    worksheet.Cells[rowIndex, 7].Value = sale.UnitPrice;
                    worksheet.Cells[rowIndex, 8].Value = sale.TotalPrice;
                    rowIndex++;
                }

                package.Save();
            }
        }
    }
}
