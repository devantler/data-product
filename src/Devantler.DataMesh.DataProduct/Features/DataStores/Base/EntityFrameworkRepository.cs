using Devantler.DataMesh.DataProduct.Features.DataStores.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.Features.DataStores.Base;

public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly DbContext _context;

    public EntityFrameworkRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(T entity, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>().AddAsync(entity, cancellationToken);
        _context.SaveChanges();
        return result.Entity.Id;
    }

    public async Task CreateBulk(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        _context.SaveChanges();
    }

    public Task Delete(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task DeleteBulk(IEnumerable<Guid> ids, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<T> Read(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<IEnumerable<T>> ReadBulk(IEnumerable<Guid> ids, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<IEnumerable<T>> ReadPaged(int page, int pageSize, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task Update(T entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task UpdateBulk(IEnumerable<T> entities, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
