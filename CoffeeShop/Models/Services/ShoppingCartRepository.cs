using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Models.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private CoffeeShopDBContext? dBContext;
        public ShoppingCartRepository(CoffeeShopDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public List<ShoppingCart>? ShoppingCart { get ; set ; }
        public string? ShoppingCartId { get; set; }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            CoffeeShopDBContext context = services.GetService<CoffeeShopDBContext>() ?? throw new Exception("Error initializing coffeeshopdbcontext");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            var ShoppingCartItem = dBContext.ShoppingCart.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
            if (ShoppingCart == null)
            {
                ShoppingCartItem = new ShoppingCart
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1
                };
                dBContext.ShoppingCart.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Qty++;
            }
            dBContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = dBContext.ShoppingCart.Where(s => s.ShoppingCartId == ShoppingCartId);
            dBContext.ShoppingCart.RemoveRange(cartItems);
            dBContext.SaveChanges();
        }

        public List<ShoppingCart> GetShoppingCart()
        {
            return ShoppingCart ??= dBContext.ShoppingCart.Where(s => s.ShoppingCartId == ShoppingCartId).
                Include(p => p.Product).ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var totalCost = dBContext.ShoppingCart.Where(s => s.ShoppingCartId == ShoppingCartId).
                Select(s => s.Product.Price * s.Qty).Sum();
            return totalCost;
        }

        public int RemoveFromCart(Product product)
        {
            var ShoppingCartItem = dBContext.ShoppingCart.FirstOrDefault(s => s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
            var Quantity = 0;
            if (ShoppingCartItem != null)
            {
                if(ShoppingCartItem.Qty > 1)
                {
                    ShoppingCartItem.Qty--;
                    Quantity = ShoppingCartItem.Qty;
                }
                else
                {
                    dBContext.ShoppingCart.Remove(ShoppingCartItem);
                }
            }
            dBContext.SaveChanges();
            return Quantity;
        }
    }
}
