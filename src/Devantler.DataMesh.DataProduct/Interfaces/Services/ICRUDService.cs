namespace Devantler.DataMesh.DataProduct.Interfaces.Services;

public interface ICRUDService<T>
{
    Task CreateAsync(T model, CancellationToken cancellationToken = default);
    Task<T?> ReadAsync(string id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T model, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
