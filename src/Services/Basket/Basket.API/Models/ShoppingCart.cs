namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string username)
    {
        Username = username;
    }
    
    // required for Mapping
    public ShoppingCart() {}
}