using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest;

/// <summary>
/// Generic interface for REST Controllers.
/// </summary>
/// <typeparam name="TSchema"></typeparam>
public interface IController<TSchema> where TSchema : class, Schemas.ISchema
{
    /// <summary>
    /// Read an entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> GetSingleAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities by id.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> GetMultipleAsync(List<string> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with pagination.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> GetMultipleWithPaginationAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with limit and offset.
    /// </summary>
    /// <param name="limit">20 by default</param>
    /// <param name="offset">0 by default</param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> GetMultipleWithLimitAsync(int limit = 20, int offset = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an entity.
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> PostSingleAsync(TSchema schema, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PostMultipleAsync(IEnumerable<TSchema> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> PutSingleAsync(TSchema schema, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PutMultipleAsync(IEnumerable<TSchema> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> DeleteSingleAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> DeleteMultipleAsync(List<string> ids, CancellationToken cancellationToken = default);
}
