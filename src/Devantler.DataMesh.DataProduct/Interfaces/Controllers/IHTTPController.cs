using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Interfaces.Controllers;

public interface IHTTPController<T>
{
    Task<ActionResult<T>> Post(T model, CancellationToken cancellationToken = default);
    Task<ActionResult<T>> Get(string id, CancellationToken cancellationToken = default);
    Task<ActionResult> Put(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Delete(string id, CancellationToken cancellationToken = default);
}
