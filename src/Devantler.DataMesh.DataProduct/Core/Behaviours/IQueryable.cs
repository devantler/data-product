namespace Devantler.DataMesh.DataProduct.Core.Behaviours;

public interface IQueryable<T>
{
    Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
