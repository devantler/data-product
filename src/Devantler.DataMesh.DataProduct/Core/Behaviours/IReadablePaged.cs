namespace Devantler.DataMesh.DataProduct.Core.Behaviours;

public interface IReadablePaged<T>
{
    Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
