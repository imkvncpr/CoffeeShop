using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IOrderRespository orderRespository;
        private IShoppingCartRepository shoppingCartRepository;
        public OrdersController(IOrderRespository orderRespository, IShoppingCartRepository shoppingCartRepository)
        {
            this.orderRespository = orderRespository;
            this.shoppingCartRepository = shoppingCartRepository;
        }
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Orders order)
        {
            orderRespository.PlaceOrder(order);
            shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);
            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
