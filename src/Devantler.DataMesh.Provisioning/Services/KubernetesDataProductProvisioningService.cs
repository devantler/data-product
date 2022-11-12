using Devantler.DataMesh.Provisioning.Interfaces;
using Devantler.DataMesh.Provisioning.Models;
using Devantler.KindCLI;

namespace Devantler.DataMesh.Provisioning.Services;

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

    public async Task TeardownAsync(Guid id)
    {
        //Delete data product with kubectl
        throw new NotImplementedException();
    }
}
