namespace Devantler.DataMesh.DataProduct.Core.Repositories;

public interface ICrudRepository<T> : IReadRepository<T>, IWriteRepository<T>
{

}
