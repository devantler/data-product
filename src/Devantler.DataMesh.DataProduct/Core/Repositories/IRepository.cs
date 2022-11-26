namespace Devantler.DataMesh.DataProduct.Core.Repositories;

public interface IRepository<T> : ICrudRepository<T>, Behaviours.IQueryable<T>
{
}
