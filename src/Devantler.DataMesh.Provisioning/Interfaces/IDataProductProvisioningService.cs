using Devantler.DataMesh.Provisioning.Models;

namespace Devantler.DataMesh.Provisioning.Interfaces;

public interface IDataProductProvisioningService
{
    public Task<string> CreateAsync(Configuration configuration);
    public Task TeardownAsync(Guid id);
}
