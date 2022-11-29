using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Core.Interfaces.Controllers;

public interface IPagedController<T>
{
    Task<ActionResult<IEnumerable<T>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}
