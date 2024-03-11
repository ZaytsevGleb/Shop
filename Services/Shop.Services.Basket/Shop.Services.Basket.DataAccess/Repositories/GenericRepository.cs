using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Context;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly ApplicationDbContext Context;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> Get(Guid id, CancellationToken ct)
        => await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyCollection<TEntity>> Get(CancellationToken ct)
    {
        var result = await DbSet.AsNoTracking().OrderByDescending(x => x.CreatedDate).ToListAsync(ct);
        return result;
    }
        

    public async Task<IReadOnlyCollection<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        => await DbSet.AsNoTracking().Where(predicate).ToListAsync(ct);

    public async Task<TEntity?> GetRandom(CancellationToken ct)
    {
        var random = Guid.NewGuid();
        return await DbSet.AsNoTracking().OrderBy(x => random).FirstOrDefaultAsync();
    }
        
    
    public async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var dbQuery = DbSet
            .AsNoTracking()
            .AsQueryable();

        if (include is not null)
        {
            dbQuery = include.Invoke(dbQuery);
        }

        return await dbQuery
            .FirstOrDefaultAsync(predicate);
    }
    
    public async Task<TEntity> Add(TEntity entity, CancellationToken ct)
    {
        await Context.AddAsync(entity, ct);
        await Context.SaveChangesAsync(ct);
        return entity;
    }
    
    public async Task AddRange(IEnumerable<TEntity> entities, CancellationToken ct)
        => await DbSet.AddRangeAsync(entities, ct);
    
    public async Task<TEntity> Update(TEntity entity, CancellationToken ct)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var entity =  await DbSet.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is not null)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync(ct);    
        }
    }
}