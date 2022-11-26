namespace Devantler.DataMesh.DataProduct.Core.Repositories;

public interface IWriteRepository<T>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
