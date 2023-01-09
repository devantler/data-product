using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// Generic interface for REST Controllers.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IController<T>
{
    // Task<ActionResult<Response<T>>> Query(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method for reading one or more entities.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ActionResult<IEnumerable<T>>> Read(IEnumerable<Guid> ids, int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method for creating one or more entities.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ActionResult<IEnumerable<Guid>>> Create(IEnumerable<T> models, CancellationToken cancellationToken = default);

    // Task<ActionResult> Update(Request<T> request, CancellationToken cancellationToken = default);

    // Task<ActionResult> Delete(Request<Guid> request, CancellationToken cancellationToken = default);
}
