namespace Devantler.DataMesh.Core.Interfaces.Repositories;

public interface IRepository<T> : ICrudRepository<T>, IQueryRepository<T>
{
}
