using MongoDB.Driver;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Abstractions;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(ReadPreference? readPreference = null) where T : IDocument;
}