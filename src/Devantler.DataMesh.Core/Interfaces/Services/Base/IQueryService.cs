using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Services.Base;

public interface IQueryService<T>
{
    Task<IEnumerable<T>> Query(string query, CancellationToken cancellationToken = default);
}
