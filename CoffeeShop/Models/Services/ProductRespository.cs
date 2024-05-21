using CoffeeShop.Models.Interfaces;

namespace CoffeeShop.Models.Services
{
    public class ProductRespository : IProductRepository
    {
        private List<Product> ProductList = new List<Product>()
        {
            new Product {Id = 1, Name = "Americano", Price =25, ProductDetail = "Meh", ImageUrl = ""},
            new Product {Id = 2, Name = "Mocha", Price = 30, ProductDetail = "Also Meh", ImageUrl = ""}
        };
        public IEnumerable<Product> GetAllProducts()
        {
            return ProductList;
        }

        public Product GetProductDetails(int id)
        {
            return ProductList.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            return ProductList.Where(p => p.IsTrendingProduct);
        }
    };
      
}
    

