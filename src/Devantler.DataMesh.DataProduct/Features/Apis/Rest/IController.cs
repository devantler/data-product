using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// Generic interface for REST Controllers.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IController<TModel> where TModel : class, IModel
{
    /// <summary>
    /// Read an entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TModel>> GetSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities by id.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TModel>>> GetMultipleAsync(List<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with pagination.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TModel>>> GetMultipleWithPaginationAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with limit and offset.
    /// </summary>
    /// <param name="limit">20 by default</param>
    /// <param name="offset">0 by default</param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TModel>>> GetMultipleWithLimitAsync(int limit = 20, int offset = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TModel>> PostSingleAsync(TModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PostMultipleAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TModel>> PutSingleAsync(TModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PutMultipleAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TModel>> DeleteSingleAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TModel>>> DeleteMultipleAsync(List<Guid> ids, CancellationToken cancellationToken = default);
}
