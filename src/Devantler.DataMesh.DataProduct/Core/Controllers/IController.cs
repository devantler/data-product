namespace Devantler.DataMesh.DataProduct.Core.Controllers;

public interface IController<T> : ICrudController<T>, IQueryController<T>
{

}
