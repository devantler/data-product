namespace Devantler.DataMesh.DataProduct.Core.Behaviours;

public interface IReadableAll<T>
{
    Task<IEnumerable<T>> ReadAllAsync(CancellationToken cancellationToken = default);
}
