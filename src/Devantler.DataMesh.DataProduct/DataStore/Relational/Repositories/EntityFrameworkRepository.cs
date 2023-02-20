using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Repositories;

/// <summary>
/// Generic repository to interact with Entity Framework relational database contexts.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    readonly DbContext _context;

    /// <summary>
    /// Base constructor for subtypes of the <see cref="EntityFrameworkRepository{T}"/> class.
    /// </summary>
    /// <param name="context"></param>
    protected EntityFrameworkRepository(DbContext context) => _context = context;

    /// <summary>
    /// Creates a single entity in a relational database.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>().AddAsync(entity, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Creates multiple entities in a relational database.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    public async Task<int> CreateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Reads a single entity from a relational database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="InvalidOperationException">Thrown when the entity is not found.</exception>
    public async Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken)
            ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");

    /// <summary>
    /// Reads multiple entities from a relational database.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    public async Task<IEnumerable<T>> ReadManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    /// <summary>
    /// Reads paged entities from a relational database.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    public async Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    /// <summary>
    /// Updates a single entity in a relational database.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>().Update(entity);
        _ = _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Updates multiple entities in a relational database.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    public async Task<int> UpdateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().UpdateRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a single entity from a relational database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public async Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken)
            ?? throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");
        var result = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Deletes multiple entities from a relational database.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        _context.Set<T>().RemoveRange(entities);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Executes a query against a relational database.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<T>> QueryAsync(string query, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
