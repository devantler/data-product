using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

/// <summary>
/// Generic repository to interact with Entity Framework relational database contexts.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityFrameworkRepository<TKey, TEntity> : IRepository<TKey, TEntity>
    where TKey : notnull
    where TEntity : class, IEntity<TKey>
{
    readonly DbContext _context;

    /// <summary>
    /// Creates a new instance of <see cref="EntityFrameworkRepository{TKey, TEntity}"/>.
    /// </summary>
    /// <param name="context"></param>
    protected EntityFrameworkRepository(DbContext context) => _context = context;

    /// <inheritdoc />
    public async Task<TEntity> CreateSingleAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<TEntity>().Add(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> CreateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var distinctEntities = entities.GroupBy(e => e.Id).Select(e => e.First());

        await _context.Set<TEntity>().AddRangeAsync(distinctEntities, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> ReadSingleAsync(TKey id, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken)
           ?? throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with id {id} not found");

    ///<inheritdoc />
    public async Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().ToListAsync(cancellationToken);

    ///<inheritdoc />
    public async Task<IQueryable<TEntity>> ReadAllAsQueryableAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult(_context.Set<TEntity>());

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> ReadMultipleAsync(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> ReadMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<TKey>> ReadMultipleIdsWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Skip((page - 1) * pageSize).Take(pageSize).Select(x => x.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> ReadMultipleWithLimitAsync(int limit, int offset, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Skip(offset).Take(limit).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<TKey>> ReadMultipleIdsWithLimitAsync(int limit, int offset, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Skip(offset).Take(limit).Select(x => x.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<TEntity> UpdateSingleAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<TEntity>().Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> UpdateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().UpdateRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken)
                     ?? throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with id {id} not found");
        var result = _context.Set<TEntity>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        _context.Set<TEntity>().RemoveRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
