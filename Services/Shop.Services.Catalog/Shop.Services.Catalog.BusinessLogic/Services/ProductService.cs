using AutoMapper;
using Microsoft.Extensions.Logging;
using Shop.Services.Catalog.BusinessLogic.Abstractions;
using Shop.Services.Catalog.BusinessLogic.Models;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.BusinessLogic.Services;

internal class ProductService(
    IMongoRepository<Product> repository,
    IMapper mapper) : IProductService
{
    public async Task<ProductModel> Get(string id, CancellationToken ct)
        => mapper.Map<ProductModel>(await repository.GetById(id, ct));

    public async Task<IEnumerable<ProductModel>> GetAll(CancellationToken ct)
        => mapper.Map<IEnumerable<ProductModel>>(await repository.Get(ct));

    public async Task<ProductModel> Add(ProductModel model, CancellationToken ct)
        => mapper.Map<ProductModel>(await repository.Add(mapper.Map<Product>(model), ct));

    public async Task<ProductModel> Update(string id, ProductModel model, CancellationToken ct)
        => mapper.Map<ProductModel>(await repository.Update(id, mapper.Map<Product>(model), ct));

    public async Task Delete(string id, CancellationToken ct)
        => await repository.Delete(id, ct);
}