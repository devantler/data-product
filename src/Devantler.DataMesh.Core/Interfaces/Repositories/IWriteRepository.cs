using System;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Repositories
{
    public interface IWriteRepository<T>
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
