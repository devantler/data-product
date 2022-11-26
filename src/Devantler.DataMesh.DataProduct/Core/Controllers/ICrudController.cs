namespace Devantler.DataMesh.DataProduct.Core.Controllers;

public interface ICrudController<T> : IReadController<T>, IWriteController<T>
{

}
