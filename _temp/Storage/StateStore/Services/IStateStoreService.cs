namespace Devantler.DataMesh.DataProduct.StateStore.Services;

public interface IStateStoreService<T> : IWriteService<T>, IReadSingleService<T>, IReadBulkService<T>, IQueryService<T>
{
}
