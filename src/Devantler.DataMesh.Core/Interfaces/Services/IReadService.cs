using Devantler.DataMesh.Core.Interfaces.Services.Base;

namespace Devantler.DataMesh.Core.Interfaces.Services
{
    public interface IReadService<T> : IReadAllService<T>, IReadBulkService<T>, IReadPagedService<T>, IReadSingleService<T>
    {
        
    }
}
