using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Controllers;

public interface IController<T> where T : Model
{
    Task<ActionResult<IEnumerable<T>>> Query(string query, CancellationToken cancellationToken = default);
    Task<ActionResult<IEnumerable<T>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<ActionResult<T>> Get(Guid id, CancellationToken cancellationToken = default);
    Task<ActionResult> Post(T entity, CancellationToken cancellationToken = default);
    Task<ActionResult> Put(T entity, CancellationToken cancellationToken = default);
    Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default);
}