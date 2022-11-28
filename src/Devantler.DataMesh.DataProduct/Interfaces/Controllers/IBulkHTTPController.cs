using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Interfaces.Controllers;

public interface IBulkHTTPController<T>
{
    public Task<ActionResult<T>> GetBulk(string ids, CancellationToken cancellationToken = default);
    public Task PostBulk(IEnumerable<T> models, CancellationToken cancellationToken = default);
    public Task PutBulk(IEnumerable<T> models, CancellationToken cancellationToken = default);
    public Task DeleteBulk(string ids, CancellationToken cancellationToken = default);
}
