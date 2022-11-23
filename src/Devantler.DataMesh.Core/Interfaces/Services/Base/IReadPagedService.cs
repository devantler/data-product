using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Services.Base
{
    public interface IReadPagedService<T>
    {
        Task<IEnumerable<T>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
