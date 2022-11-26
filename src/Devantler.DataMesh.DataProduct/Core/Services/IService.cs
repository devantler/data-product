namespace Devantler.DataMesh.DataProduct.Core.Services;

public interface IService<T> : ICrudService<T>, IQueryable<T>
{

}
