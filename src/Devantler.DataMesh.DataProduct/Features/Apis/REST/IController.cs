using Devantler.DataMesh.DataProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.REST;

public interface IController<T> where T : IModel
{
    Task<ActionResult<IEnumerable<T>>> Query(string query, CancellationToken cancellationToken = default);

    Task<ActionResult<T>> Get(Guid id, CancellationToken cancellationToken = default);

    Task<ActionResult<IEnumerable<T>>> GetBulk(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    Task<ActionResult<IEnumerable<T>>> GetPaged(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<ActionResult<Guid>> Post(T model, CancellationToken cancellationToken = default);

    Task<ActionResult> PostBulk(IEnumerable<T> models, CancellationToken cancellationToken = default);

    Task<ActionResult> Put(T model, CancellationToken cancellationToken = default);

    Task<ActionResult> PutBulk(IEnumerable<T> models, CancellationToken cancellationToken = default);

    Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default);

    Task<ActionResult> DeleteBulk(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
