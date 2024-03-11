using System.Linq.Expressions;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Abstractions;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    Task<TDocument> Add(TDocument document, CancellationToken ct);
    Task<IEnumerable<TDocument>> Get(CancellationToken ct);
    Task<IEnumerable<TDocument>> Get(Expression<Func<TDocument, bool>> predicate, CancellationToken ct);
    Task<TDocument> GetById(string id, CancellationToken ct);
    Task<TDocument> Update(string id, TDocument document, CancellationToken ct);
    Task Delete(string id, CancellationToken ct);
}