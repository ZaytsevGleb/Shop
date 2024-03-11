using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Abstractions;

public interface IGenericRepository<TEntity> where TEntity: BaseEntity
{
    Task<TEntity?> Get(Guid id, CancellationToken ct);
    Task<IReadOnlyCollection<TEntity>> Get(CancellationToken ct);
    Task<IReadOnlyCollection<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
    Task<TEntity> Add(TEntity entity, CancellationToken ct);
    Task AddRange(IEnumerable<TEntity> entities, CancellationToken ct);
    Task<TEntity> Update(TEntity entity, CancellationToken ct);
    Task Delete(Guid id, CancellationToken ct);
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
}