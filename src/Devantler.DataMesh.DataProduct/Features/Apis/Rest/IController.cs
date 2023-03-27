using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest;

/// <summary>
/// Generic interface for REST Controllers.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
public interface IController<TKey, TSchema> where TSchema : class, Schemas.ISchema<TKey>
{
    /// <summary>
    /// Read an entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> GetById(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities by id.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> GetByIds(List<TKey> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with pagination.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> GetPageAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads multiple entities with pagination.
    /// </summary>
    /// <remarks>
    /// It uses the limit and offset format. It means that it will return a limited number of entities starting from the offset.
    /// </remarks>
    /// <param name="limit">20 by default</param>
    /// <param name="offset">0 by default</param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> GetByOffset(int offset = 0, int limit = 20, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PostAsync(IEnumerable<TSchema> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<int>> PutAsync(IEnumerable<TSchema> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<TSchema>> DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    Task<ActionResult<IEnumerable<TSchema>>> DeleteAsync(List<TKey> ids, CancellationToken cancellationToken = default);
}
