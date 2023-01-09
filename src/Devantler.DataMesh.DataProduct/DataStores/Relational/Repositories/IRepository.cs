namespace Devantler.DataMesh.DataProduct.DataStores.Relational.Repositories;

/// <summary>
/// Generic interface for repositories, with common functionality needed to interact with relational databases.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : IEntity
{
    // Task<Guid> Create(T entity, CancellationToken cancellationToken = default);
    // Task CreateMany(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method to read a single entity from a relational database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> Read(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method to read multiple entities from a relational database.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> ReadMany(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method to read paged entities from a relational database.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> ReadPaged(int page, int pageSize, CancellationToken cancellationToken = default);

    // Task Update(T entity, CancellationToken cancellationToken = default);
    // Task UpdateMany(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    // Task Delete(Guid id, CancellationToken cancellationToken = default);
    // Task DeleteMany(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    // Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
