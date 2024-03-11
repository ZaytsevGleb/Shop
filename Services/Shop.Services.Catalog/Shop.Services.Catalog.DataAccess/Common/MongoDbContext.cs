using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Entities;
using Shop.Services.Catalog.DataAccess.Options;

namespace Shop.Services.Catalog.DataAccess.Common;

public class MongoDbContext : IMongoDbContext
{
    private IMongoDatabase _db;

    public MongoDbContext(IOptions<DatabaseOptions> settings)
    {
        _db = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(ReadPreference? readPreference = null) where T : IDocument
        => _db.WithReadPreference(readPreference ?? ReadPreference.Primary)
            .GetCollection<T>(GetCollectionName<T>());

    public static string GetCollectionName<T>() where T : IDocument
        => (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as
            BsonCollectionAttribute)?.CollectionName;
}