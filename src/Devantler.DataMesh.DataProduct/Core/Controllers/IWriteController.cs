using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Core.Controllers;

public interface IWriteController<T>
{
    Task<ActionResult> Post(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Put(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default);
}
