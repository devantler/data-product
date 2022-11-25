using System;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Services;

public interface IWriteService<T>
{
    Task CreateAsync(T model, CancellationToken cancellationToken = default);
    Task UpdateAsync(T model, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
