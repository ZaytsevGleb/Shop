using Shop.Services.Catalog.DataAccess.Common;

namespace Shop.Services.Catalog.DataAccess.Entities;

[BsonCollection(nameof(Product))]
public class Product : Document
{
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}