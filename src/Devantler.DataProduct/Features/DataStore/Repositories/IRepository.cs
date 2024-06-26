using Devantler.DataProduct.Features.DataStore.Entities;

namespace Devantler.DataProduct.Features.DataStore.Repositories;

/// <summary>
/// Generic interface for repositories, with common functionality needed to interact with databases.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
{
  /// <summary>
  /// Creates a single entity in a data store.
  /// </summary>
  /// <param name="entity"></param>
  /// <param name="cancellationToken"></param>
  Task<TEntity?> CreateSingleAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  /// Creates multiple entities in a data store.
  /// </summary>
  /// <param name="entities"></param>
  /// <param name="insertIfNotExists"></param>
  /// <param name="cancellationToken"></param>
  Task<IEnumerable<TEntity>> CreateMultipleAsync(IEnumerable<TEntity> entities, bool insertIfNotExists, CancellationToken cancellationToken = default);

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
  /// Reads all ids for entities in a data store.
  /// </summary>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  Task<IEnumerable<TKey>> ReadAllIdsAsync(CancellationToken cancellationToken = default);

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
  Task<IEnumerable<TEntity>> ReadMultipleWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);

  /// <summary>
  /// Reads ids of paged entities from a relational database.
  /// </summary>
  /// <param name="page"></param>
  /// <param name="pageSize"></param>
  /// <param name="cancellationToken"></param>
  Task<IEnumerable<TKey>> ReadMultipleIdsWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default);

  /// <summary>
  /// Reads entities from a relational database with a limit and an offset.
  /// </summary>
  /// <param name="limit"></param>
  /// <param name="offset"></param>
  /// <param name="cancellationToken"></param>
  Task<IEnumerable<TEntity>> ReadMultipleWithLimitAsync(int limit, int offset, CancellationToken cancellationToken = default);

  /// <summary>
  /// Reads ids of entities from a relational database with a limit and an offset.
  /// </summary>
  /// <param name="limit"></param>
  /// <param name="offset"></param>
  /// <param name="cancellationToken"></param>
  Task<IEnumerable<TKey>> ReadMultipleIdsWithLimitAsync(int limit, int offset, CancellationToken cancellationToken = default);

  /// <summary>
  /// Updates a single entity in a data store.
  /// </summary>
  /// <param name="entity"></param>
  /// <param name="cancellationToken"></param>
  Task UpdateSingleAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  /// Updates multiple entities in a data store.
  /// </summary>
  /// <param name="entities"></param>
  /// <param name="cancellationToken"></param>
  Task UpdateMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

  /// <summary>
  /// Deletes a single entity from a data store.
  /// </summary>
  /// <param name="id"></param>
  /// <param name="cancellationToken"></param>
  Task DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default);

  /// <summary>
  /// Deletes multiple entities from a data store.
  /// </summary>
  /// <param name="ids"></param>
  /// <param name="cancellationToken"></param>
  Task DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
}
