using Devantler.DataMesh.DataProduct.Models;

namespace Devantler.DataMesh.DataProduct.Services;

public interface IService<T> where T : Model
{
    Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
    Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
