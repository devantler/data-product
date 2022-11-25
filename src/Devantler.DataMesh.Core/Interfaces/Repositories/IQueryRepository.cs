using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Repositories;

public interface IQueryRepository<T>
{
    Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
