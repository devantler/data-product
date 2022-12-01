using Devantler.DataMesh.DataProduct.Core.Entities;

namespace Devantler.DataMesh.DataProduct.Core.Base.Repositories;

public interface ICRUDRepository<T> where T : IEntity
{
    void CreateAsync(T entity, CancellationToken cancellationToken = default);
    T ReadAsync(Guid id, CancellationToken cancellationToken = default);
    void UpdateAsync(T entity, CancellationToken cancellationToken = default);
    void DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
