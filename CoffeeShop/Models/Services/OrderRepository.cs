using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;

namespace CoffeeShop.Models.Services
{
    public class OrderRepository : IOrderRespository
    {
        private CoffeeShopDBContext dBContext;
        private IShoppingCartRepository shoppingCartRepository;

        public OrderRepository(CoffeeShopDBContext dBContext, IShoppingCartRepository shoppingCartRepository)
        {
            this.dBContext = dBContext;
            this.shoppingCartRepository = shoppingCartRepository;
        }
        public void PlaceOrder(Orders order)
        {
           var shoppingCartItems = shoppingCartRepository.GetShoppingCart();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetails = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };
                order.OrderPlaced = DateTime.Now;
                order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
                dBContext.Orders.Add(order);
                dBContext.SaveChanges();
            }
        }
    }
}
