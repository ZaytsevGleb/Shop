namespace Shop.Services.Catalog.WebAPI.Dtos;

public class ShpppingCartDto
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItemDto> Items { get; set; } = new();

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