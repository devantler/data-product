using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

/// <summary>
/// Generic interface for repositories, with common functionality needed to interact with databases.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
{
    /// <summary>
    /// Creates a single entity in a data store.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    Task<TEntity> CreateSingleAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities in a data store.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    Task<int> CreateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a single entity from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TEntity> ReadSingleAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads all entities from a data store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads all entities as queryable objects from a data store.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IQueryable<TEntity>> ReadAllAsQueryableAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Read multiple entities from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TEntity>> ReadMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads paged entities from a relational database.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TEntity>> ReadMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads entities from a relational database with a limit and an offset.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <param name="cancellationToken"></param>
    Task<IEnumerable<TEntity>> ReadMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a single entity in a data store.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    Task<TEntity> UpdateSingleAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities in a data store.
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    Task<int> UpdateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a single entity from a data store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<TEntity> DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities from a data store.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<int> DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
}
