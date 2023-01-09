using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational.Repositories;

/// <summary>
/// Generic repository to interact with Entity Framework relational database contexts.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    readonly DbContext _context;

    /// <summary>
    /// Constructor to construct the Entity Framework repository.
    /// </summary>
    /// <param name="context"></param>
    protected EntityFrameworkRepository(DbContext context) => _context = context;

    /// <summary>
    /// Abstract method to read a single entity from a relational database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<T> Read(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken) ??
        throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found");

    /// <summary>
    /// Abstract method to read multiple entities from a relational database.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> ReadMany(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    /// <summary>
    /// Abstract method to read paged entities from a relational database.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> ReadPaged(int page, int pageSize, CancellationToken cancellationToken = default)
        => await _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
}
