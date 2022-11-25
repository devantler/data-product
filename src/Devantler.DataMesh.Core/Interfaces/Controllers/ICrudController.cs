namespace Devantler.DataMesh.Core.Interfaces.Controllers;

public interface ICrudController<T> : IReadController<T>, IWriteController<T>
{

}
