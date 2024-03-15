namespace Shop.Services.Catalog.DataAccess.Entities;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShippingCartItem> Items { get; set; } = new();

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            Items.ForEach(x => { totalPrice += x.Price * x.Quantity; });
            return totalPrice;
        }
    }
}