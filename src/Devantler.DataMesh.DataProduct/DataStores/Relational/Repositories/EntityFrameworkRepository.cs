using Devantler.DataMesh.DataProduct.DataStores.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational.Repositories;

public abstract class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly DbContext _context;

    protected EntityFrameworkRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<T> Read(Guid id, CancellationToken cancellationToken = default) =>
      await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken) ?? throw new Exception($"Entity of type {typeof(T).Name} with id {id} not found");

    public async Task<IEnumerable<T>> ReadMany(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
      => await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> ReadPaged(int page, int pageSize, CancellationToken cancellationToken = default)
      => await _context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
}
