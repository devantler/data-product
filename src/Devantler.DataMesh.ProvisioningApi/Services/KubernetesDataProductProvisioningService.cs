using Devantler.DataMesh.ProvisioningApi.Interfaces;
using Devantler.DataMesh.ProvisioningApi.Models;
using Devantler.KindCLI;

namespace Devantler.DataMesh.ProvisioningApi.Services;

public class K8sDataProductProvisioningService : IDataProductProvisioningService
{
    private readonly IKindCliService _kindCliService;

    public K8sDataProductProvisioningService(IKindCliService kindCliService)
    {
        _kindCliService = kindCliService;
    }

    public async Task<string> CreateAsync(Configuration configuration)
    {
        if (configuration.ClusterInfo.IsLocal)
            await _kindCliService.CreateClusterAsync(configuration.ClusterInfo.Name);

        var id = Guid.NewGuid();
        //Create data product with kubectl
        return id.ToString();
    }

    public Task TeardownAsync(Guid id)
    {
        //Delete data product with kubectl
        throw new NotImplementedException();
    }
}
