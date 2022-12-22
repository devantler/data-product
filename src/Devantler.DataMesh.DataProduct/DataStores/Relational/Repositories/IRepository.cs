using Devantler.DataMesh.DataProduct.DataStores.Relational.Entities;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational.Repositories;

public interface IRepository<T> where T : IEntity
{
    // Task<Guid> Create(T entity, CancellationToken cancellationToken = default);
    // Task CreateMany(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<T> Read(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> ReadMany(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> ReadPaged(int page, int pageSize, CancellationToken cancellationToken = default);
    // Task Update(T entity, CancellationToken cancellationToken = default);
    // Task UpdateMany(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    // Task Delete(Guid id, CancellationToken cancellationToken = default);
    // Task DeleteMany(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    // Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
