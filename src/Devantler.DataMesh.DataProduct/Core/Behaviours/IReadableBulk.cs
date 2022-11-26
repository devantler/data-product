namespace Devantler.DataMesh.DataProduct.Core.Behaviours;

public interface IReadableBulk<T>
{
    Task<IEnumerable<T>> ReadBulkAsync(Guid[] ids, CancellationToken cancellationToken = default);
}
