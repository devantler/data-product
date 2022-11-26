namespace Devantler.DataMesh.DataProduct.Core.Services;

public interface ICrudService<T> : IReadService<T>, IWriteService<T>
{
}
