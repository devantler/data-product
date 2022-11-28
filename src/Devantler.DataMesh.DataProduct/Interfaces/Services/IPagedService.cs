namespace Devantler.DataMesh.DataProduct.Interfaces.Services;

public interface IPagedService<T>
{
    Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
