using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Core.Controllers;

public interface IReadController<T>
{
    Task<ActionResult<IEnumerable<T>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<ActionResult<IEnumerable<T>>> GetAll(CancellationToken cancellationToken = default);
    Task<ActionResult<T>> Get(Guid id, CancellationToken cancellationToken = default);
}
