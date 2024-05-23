using CoffeeShop.Models.Interfaces;
using CoffeeShop.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;
        public ProductsController(IProductRepository ProductRespository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Shop()
        {
            return View(productRepository.GetAllProducts);
        }
        public IActionResult Detail(int id)
        {
            var product = (productRepository.GetProductDetails(id));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
