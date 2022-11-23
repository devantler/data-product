using Devantler.DataMesh.Core.Interfaces.Services;
using Devantler.DataMesh.Core.Interfaces.Services.Base;

namespace Devantler.DataMesh.DataProduct.Features.StateStore.Services;

public interface IStateStoreService<T> : IWriteService<T>, IReadSingleService<T>, IReadBulkService<T>, IQueryService<T>
{
}
