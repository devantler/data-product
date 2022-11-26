namespace Devantler.DataMesh.DataProduct.Core.Behaviours;

public interface IReadableSingle<T>
{
    Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default);
}
