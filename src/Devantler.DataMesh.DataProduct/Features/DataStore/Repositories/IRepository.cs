using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

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
    Task<T> CreateSingleAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities in a data store.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    Task<int> CreateMultipleAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single entity from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<T> ReadSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads all entities from a data store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> ReadAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads all entities as queryable objects from a data store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IQueryable<T>> ReadAllAsQueryableAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Read multiple entities from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<T>> ReadMultipleAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads paged entities from a relational database.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<T>> ReadMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads entities from a relational database with a limit and an offset.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<T>> ReadMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a single entity in a data store.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    Task<T> UpdateSingleAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities in a data store.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    Task<int> UpdateMultipleAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a single entity from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<T> DeleteSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
