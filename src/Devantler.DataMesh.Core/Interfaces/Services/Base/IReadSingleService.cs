using System;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Services.Base
{
    public interface IReadSingleService<T>
    {
        Task<T> ReadAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
