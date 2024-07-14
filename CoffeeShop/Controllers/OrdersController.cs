using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoffeeShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRespository _orderRespository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrdersController(IOrderRespository orderRespository, IShoppingCartRepository shoppingCartRepository)
        {
            _orderRespository = orderRespository ?? throw new ArgumentNullException(nameof(orderRespository));
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        public IActionResult Checkout()
        {
            var shoppingCartItems = _shoppingCartRepository.GetShoppingCart();
            if (shoppingCartItems == null || shoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty. Please add some items before checking out.");
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Orders order)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(order);
                }

                var shoppingCartItems = _shoppingCartRepository.GetShoppingCart();
                if (shoppingCartItems == null || shoppingCartItems.Count == 0)
                {
                    ModelState.AddModelError("", "Your cart is empty. Please add some items before checking out.");
                    return View(order);
                }

                order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();
                order.OrderPlaced = DateTime.Now;

                _orderRespository.PlaceOrder(order);
                _shoppingCartRepository.ClearCart();
                HttpContext.Session.SetInt32("CartCount", 0);

                return RedirectToAction("CheckoutComplete");
            }
            catch (Exception ex)
            {
                // Log the exception
                // logger.LogError(ex, "Error occurred while processing order");

                ModelState.AddModelError("", "An error occurred while processing your order. Please try again.");
                return View(order);
            }
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious coffee!";
            return View();
        }
    }
}