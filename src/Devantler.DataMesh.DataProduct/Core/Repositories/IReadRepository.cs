using Devantler.DataMesh.DataProduct.Core.Behaviours;

namespace Devantler.DataMesh.DataProduct.Core.Repositories;

public interface IReadRepository<T> : IReadableSingle<T>, IReadablePaged<T>, IReadableBulk<T>, IReadableAll<T>
{
}
