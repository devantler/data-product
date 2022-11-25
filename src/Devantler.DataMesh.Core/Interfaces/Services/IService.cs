using Devantler.DataMesh.Core.Interfaces.Services.Base;

namespace Devantler.DataMesh.Core.Interfaces.Services;

public interface IService<T> : ICrudService<T>, IQueryService<T>
{

}
