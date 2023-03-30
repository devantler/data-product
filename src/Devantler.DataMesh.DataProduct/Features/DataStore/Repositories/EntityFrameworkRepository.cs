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
    public async Task<IEnumerable<TEntity>> CreateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        //Bulk insert is only supported by SQL Server and PostgreSQL
        if (_context.Database.IsNpgsql())
        {
            await _context.Set<TEntity>().BulkInsertAsync(entities, cancellationToken);
        }
        else
        {
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        await _context.BulkSaveChangesAsync(cancellationToken);

        return await _context.Set<TEntity>().BulkReadAsync(entities, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> ReadSingleAsync(TKey id, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken)
           ?? throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with id {id} not found");

    ///<inheritdoc />
    public async Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().ToListAsync(cancellationToken);

    ///<inheritdoc />
    public async Task<IEnumerable<TKey>> ReadAllIdsAsync(CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Select(x => x.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> ReadMultipleAsync(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().BulkReadAsync(ids, cancellationToken);

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
    public async Task UpdateSingleAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _ = _context.Set<TEntity>().Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().BulkUpdateAsync(entities, cancellationToken);
        await _context.BulkSaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken)
                     ?? throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with id {id} not found");
        _ = _context.Set<TEntity>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        await _context.Set<TEntity>().BulkDeleteAsync(entities, cancellationToken);
        await _context.BulkSaveChangesAsync(cancellationToken);
    }
}
