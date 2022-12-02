namespace Devantler.DataMesh.DataProduct.Core.Base.Services;

public interface IPagedService<T>
{
    Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}
