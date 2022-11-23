namespace Devantler.DataMesh.Core.Interfaces.Services
{
    public interface ICrudService<T> : IReadService<T>, IWriteService<T>
    {
    }
}
