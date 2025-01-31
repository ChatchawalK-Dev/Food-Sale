using Microsoft.EntityFrameworkCore;
using Food_Sale.Models;

namespace Food_Sale.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<FoodSale> FoodSales { get; set; }
    }
}