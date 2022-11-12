using Devantler.DataMesh.Provisioning.Interfaces;
using Devantler.DataMesh.Provisioning.Services;
using Devantler.KindCLI;

namespace Devantler.DataMesh.Provisioning.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddK8sDataProductProvisioningService(this IServiceCollection services)
    {
        services.AddKindCliService();
        services.AddScoped<IDataProductProvisioningService, K8sDataProductProvisioningService>();
        return services;
    }
}
