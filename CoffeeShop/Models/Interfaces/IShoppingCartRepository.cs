namespace CoffeeShop.Models.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddToCart(Product product);
        int RemoveFromCart(Product product);
        List<ShoppingCart> GetShoppingCart();
        void ClearCart();
        decimal GetShoppingCartTotal();  
        public List<ShoppingCart> ShoppingCart { get; set; }
    }
}
