using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

public interface IController<T> where T : IModel
{
    // Task<ActionResult<Response<T>>> Query(string query, CancellationToken cancellationToken = default);

    Task<ActionResult<IEnumerable<T>>> Get(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    Task<ActionResult<IEnumerable<T>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    // Task<ActionResult<Response<Guid>>> Post(Request<T> request, CancellationToken cancellationToken = default);

    // Task<ActionResult> Put(Request<T> request, CancellationToken cancellationToken = default);

    // Task<ActionResult> Delete(Request<Guid> request, CancellationToken cancellationToken = default);
}
