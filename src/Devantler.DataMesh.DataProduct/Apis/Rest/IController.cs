using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

public interface IController<T>
{
    // Task<ActionResult<Response<T>>> Query(string query, CancellationToken cancellationToken = default);

    Task<ActionResult<IEnumerable<T>>> Read(IEnumerable<Guid> id, int page = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);

    Task<ActionResult<IEnumerable<Guid>>> Create(IEnumerable<T> models, CancellationToken cancellationToken = default);

    // Task<ActionResult> Update(Request<T> request, CancellationToken cancellationToken = default);

    // Task<ActionResult> Delete(Request<Guid> request, CancellationToken cancellationToken = default);
}
