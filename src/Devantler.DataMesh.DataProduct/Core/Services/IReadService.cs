using Devantler.DataMesh.DataProduct.Core.Behaviours;

namespace Devantler.DataMesh.DataProduct.Core.Services;

public interface IReadService<T> : IReadableSingle<T>, IReadablePaged<T>, IReadableBulk<T>, IReadableAll<T>
{

}
