using Devantler.DataMesh.DataProduct.Core.Models;

namespace Devantler.DataMesh.DataProduct.Core.Base.Services;

public interface ICRUDService<T> where T : IModel
{
    Task CreateAsync(T model, CancellationToken cancellationToken = default);
    Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T model, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
