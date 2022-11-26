using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Core.Controllers;

public interface IQueryController<T>
{
    Task<ActionResult<IEnumerable<T>>> Query(string query, CancellationToken cancellationToken = default);
}
