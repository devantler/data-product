using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Devantler.DataMesh.Core.Interfaces.Services.Base;

public interface IReadBulkService<T>
{
    Task<IEnumerable<T>> ReadBulkAsync(Guid[] ids, CancellationToken cancellationToken = default);
}
