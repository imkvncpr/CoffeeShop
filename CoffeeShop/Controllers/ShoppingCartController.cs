using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCartRepository ShoppingCartRepository;
        private IProductRepository productRepository;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            this.ShoppingCartRepository = shoppingCartRepository;
        }
        public IActionResult Index()
        {
            var items = ShoppingCartRepository.GetShoppingCart();
            ShoppingCartRepository.ShoppingCart = items;
            ViewBag.CartTotal = ShoppingCartRepository.GetShoppingCartTotal();
            return View(items);
        }

        public RedirectToActionResult AddToShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
            if (product != null)
            {
                ShoppingCartRepository.AddToCart(product);
                int cartCount = ShoppingCartRepository.GetShoppingCart().Count;
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            return RedirectToAction("index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pId)
        {
            var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
            if (product != null)
            {
            }
            return RedirectToAction("index");
        }
    }
}
