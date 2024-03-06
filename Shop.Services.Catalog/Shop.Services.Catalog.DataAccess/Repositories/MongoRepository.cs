using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Entities;
using Shop.Services.Catalog.Shared.Exceptions;

namespace Shop.Services.Catalog.DataAccess.Repositories;

public class MongoRepository<TDocument>(IMongoDbContext context, ILogger<MongoRepository<TDocument>> logger)
    : IMongoRepository<TDocument>
    where TDocument : IDocument
{
    private readonly IMongoCollection<TDocument> _collection = context.GetCollection<TDocument>();
    private readonly ILogger _logger = logger;

    public async Task<TDocument> Add(TDocument document, CancellationToken ct)
    {
        await _collection.InsertOneAsync(document, cancellationToken: ct);
        return document;
    }

    public async Task<IEnumerable<TDocument>> Get(CancellationToken ct)
    {
        _logger.LogInformation("Get all {type} documents", typeof(TDocument).ToString());
        var all = await _collection.FindAsync(_ => true, cancellationToken: ct);
        return await all.ToListAsync(ct);
    }

    public async Task<IEnumerable<TDocument>> Get(Expression<Func<TDocument, bool>> predicate, CancellationToken ct)
    {
        _logger.LogInformation("Get by expression {type} documents", typeof(TDocument).ToString());
        var result = await _collection.FindAsync(predicate, cancellationToken: ct);
        return await result.ToListAsync(ct);
    }

    public async Task<TDocument> GetById(string id, CancellationToken ct)
    {
        _logger.LogInformation("Get document by id {type}, id: {id}", typeof(TDocument).ToString(), id);
        var document = await _collection.FindAsync(x => x.Id == id, cancellationToken: ct);
        var result = await document.FirstOrDefaultAsync(ct);

        if (result is not null) return result;

        _logger.LogError("Document of type {type} with {id} not found", typeof(TDocument).ToString(), id);
        throw new VariableNullException(nameof(TDocument));
    }

    public async Task<TDocument> Update(string id, TDocument document, CancellationToken ct)
    {
        _logger.LogInformation("Update {type} document, id: {id}", typeof(TDocument).ToString(), id);
        await _collection.ReplaceOneAsync(x => x.Id == id, document, cancellationToken: ct);
        return document;
    }

    public async Task Delete(string id, CancellationToken ct)
    {
        await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken: ct);
    }
}