using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;


namespace CoffeeShop.Data
{
    public class CoffeeShopDBContext : DbContext
    {
        public CoffeeShopDBContext(DbContextOptions<CoffeeShopDBContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Mocha", Price = 15, IsTrendingProduct = true, ImageUrl = ""}
                );
        }
    }
}
