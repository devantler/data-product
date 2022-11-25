using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Services.Base;

public interface IReadAllService<T>
{
    Task<IEnumerable<T>> ReadAllAsync(CancellationToken cancellationToken = default);
}
