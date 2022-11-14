using Devantler.DataMesh.ProvisioningApi.Models;

namespace Devantler.DataMesh.ProvisioningApi.Interfaces;

public interface IDataProductProvisioningService
{
    public Task<string> CreateAsync(Configuration configuration);
    public Task TeardownAsync(Guid id);
}
