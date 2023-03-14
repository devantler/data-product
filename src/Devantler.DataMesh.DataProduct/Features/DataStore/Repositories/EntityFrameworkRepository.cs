using System.Diagnostics.CodeAnalysis;
using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

/// <summary>
/// Generic repository to interact with Entity Framework relational database contexts.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EntityFrameworkRepository<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : IRepository<T> where T : class, IEntity
{
    readonly DbContext _context;

    /// <summary>
    /// Creates a new instance of <see cref="EntityFrameworkRepository{T}"/>.
    /// </summary>
    /// <param name="context"></param>
    protected EntityFrameworkRepository(DbContext context) => _context = context;

    /// <inheritdoc />
    public async Task<T> CreateSingleAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>().Add(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> CreateMultipleAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var filteredEntities = new List<T>();
        foreach (var entity in entities)
        {
            bool exists = await _context.Set<T>().AnyAsync(e => e.Id == entity.Id, cancellationToken)
                || filteredEntities.Any(e => e.Id == entity.Id);

            if (exists) continue;

            filteredEntities.Add(entity);
        }
        await _context.Set<T>().AddRangeAsync(filteredEntities, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<T> ReadSingleAsync(string id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken)
           ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");

    ///<inheritdoc />
    public async Task<IEnumerable<T>> ReadAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<T>().ToListAsync(cancellationToken);

    ///<inheritdoc />
    public async Task<IQueryable<T>> ReadAllAsQueryableAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult(_context.Set<T>());

    /// <inheritdoc />
    public async Task<IEnumerable<T>> ReadMultipleAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
        => await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> ReadMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
        => await _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> ReadMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default)
        => await _context.Set<T>().Skip(offset).Take(limit).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<T> UpdateSingleAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>().Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> UpdateMultipleAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().UpdateRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<T> DeleteSingleAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken)
                     ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");
        var result = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> DeleteMultipleAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        _context.Set<T>().RemoveRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
