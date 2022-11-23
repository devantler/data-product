namespace Devantler.DataMesh.Core.Interfaces.Repositories
{
    public interface ICrudRepository<T> : IReadRepository<T>, IWriteRepository<T>
    {

    }
}
