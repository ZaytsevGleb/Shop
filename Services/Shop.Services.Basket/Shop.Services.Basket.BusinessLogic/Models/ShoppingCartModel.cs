namespace Shop.Services.Catalog.BusinessLogic.Models;

public class ShoppingCartModel
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItemModel> Items { get; set; } = new();

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