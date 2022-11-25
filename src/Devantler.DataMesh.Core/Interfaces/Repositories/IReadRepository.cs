using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Repositories;

public interface IReadRepository<T>
{
    Task<IEnumerable<T>> ReadAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default);
}
