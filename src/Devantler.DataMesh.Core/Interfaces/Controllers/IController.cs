namespace Devantler.DataMesh.Core.Interfaces.Controllers
{
    public interface IController<T> : ICrudController<T>, IQueryController<T>
    {

    }
}
