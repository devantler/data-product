namespace Devantler.DataMesh.DataProduct.Interfaces.Services;

public interface IBulkCRUDService<T>
{
    Task BulkCreateAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> BulkReadAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    Task BulkUpdateAsync(IEnumerable<T> models, CancellationToken cancellationToken = default);
    Task BulkDeleteAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);

}
