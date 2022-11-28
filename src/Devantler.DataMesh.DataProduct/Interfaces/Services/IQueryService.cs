namespace Devantler.DataMesh.DataProduct.Interfaces.Services;

public interface IQueryService<T>
{
    Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
