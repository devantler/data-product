namespace Devantler.DataMesh.DataProduct.DataStore.Interfaces;

/// <summary>
/// Generic interface for repositories, with common functionality needed to interact with databases.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : IEntity
{
    /// <summary>
    /// Creates a single entity in a data store.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities in a data store.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    Task<int> CreateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single entity from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read multiple entities from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<T>> ReadManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method to read paged entities from a relational database.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a single entity in a data store.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities in a data store.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    Task<int> UpdateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a single entity from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<int> DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a query against the data store.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<T>> QueryAsync(string query, CancellationToken cancellationToken = default);
}
