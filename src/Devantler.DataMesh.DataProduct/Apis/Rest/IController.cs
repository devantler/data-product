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
    Task<ActionResult<T>> ReadAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities by id.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<T>>> ReadManyAsync(List<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with pagination.
    /// </summary>
    /// <param name="pages"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<T>>> ReadPagedAsync(int pages = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> CreateAsync(T model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> CreateManyAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> UpdateAsync(T model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> UpdateManyAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<T>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<T>>> DeleteManyAsync(List<Guid> ids, CancellationToken cancellationToken = default);
}
