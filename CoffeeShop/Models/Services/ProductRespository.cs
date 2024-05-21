using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;

namespace CoffeeShop.Models.Services
{
    public class ProductRespository : IProductRepository
    {
        private CoffeeShopDBContext dBContext;
        public ProductRespository(CoffeeShopDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return dBContext.Products;
        }

        public Product GetProductDetails(int id)
        {
            return dBContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            return dBContext.Products.Where(p => p.IsTrendingProduct);
        }
    };
      
}
    

