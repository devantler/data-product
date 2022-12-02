namespace Devantler.DataMesh.DataProduct.Core.Base.Services;

public interface IQueryService<T>
{
    Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
