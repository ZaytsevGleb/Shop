using Shop.Services.Catalog.BusinessLogic.Models;

namespace Shop.Services.Catalog.BusinessLogic.Abstractions;

public interface IProductService
{
    Task<ProductModel> Get(string id, CancellationToken ct);
    Task<IEnumerable<ProductModel>> GetAll(CancellationToken ct);
    Task<ProductModel> Add(ProductModel model, CancellationToken ct);
    Task<ProductModel> Update(string id, ProductModel model, CancellationToken ct);
    Task Delete(string id, CancellationToken ct);
}