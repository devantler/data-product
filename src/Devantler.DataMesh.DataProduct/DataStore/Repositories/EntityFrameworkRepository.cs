using Devantler.DataMesh.DataProduct.DataStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.Repositories;

/// <summary>
/// Generic repository to interact with Entity Framework relational database contexts.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    readonly DbContext _context;

    /// <inheritdoc />
    protected EntityFrameworkRepository(DbContext context) => _context = context;

    /// <inheritdoc />
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>().AddAsync(entity, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> CreateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken)
            ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");

    /// <inheritdoc />
    public async Task<IEnumerable<T>> ReadManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> ReadListAsync(int limit, int offset, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Skip(offset).Take(limit).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>().Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> UpdateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().UpdateRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken)
            ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");
        var result = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<int> DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        _context.Set<T>().RemoveRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<IEnumerable<T>> QueryAsync(string query, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
