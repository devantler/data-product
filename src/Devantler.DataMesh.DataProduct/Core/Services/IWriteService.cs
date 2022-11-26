namespace Devantler.DataMesh.DataProduct.Core.Services;

public interface IWriteService<T>
{
    Task CreateAsync(T model, CancellationToken cancellationToken = default);
    Task UpdateAsync(T model, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
