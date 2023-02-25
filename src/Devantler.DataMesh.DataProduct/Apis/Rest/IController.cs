using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// Generic interface for REST Controllers.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IController<T>
{
    /// <summary>
    /// Read an entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> GetSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities by id.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IQueryable<T>>> GetMultipleAsync(List<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with pagination.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IQueryable<T>>> GetMultipleWithPaginationAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with limit and offset.
    /// </summary>
    /// <param name="limit">20 by default</param>
    /// <param name="offset">0 by default</param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IQueryable<T>>> GetMultipleWithLimitAsync(int limit = 20, int offset = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> PostSingleAsync(T model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PostMultipleAsync(IQueryable<T> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> PutSingleAsync(T model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PutMultipleAsync(IQueryable<T> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> DeleteSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IQueryable<T>>> DeleteMultipleAsync(List<Guid> ids, CancellationToken cancellationToken = default);
}
