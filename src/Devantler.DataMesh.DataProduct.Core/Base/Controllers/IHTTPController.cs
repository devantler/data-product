using Devantler.DataMesh.DataProduct.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Core.Base.Controllers;

public interface IHTTPController<T> where T : IModel
{
    Task<ActionResult<T>> Get(Guid id, CancellationToken cancellationToken = default);
    Task<ActionResult> Post(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Put(T model, CancellationToken cancellationToken = default);
    Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default);
}
