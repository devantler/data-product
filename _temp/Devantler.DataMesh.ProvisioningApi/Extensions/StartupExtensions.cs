using Devantler.DataMesh.ProvisioningApi.Interfaces;
using Devantler.DataMesh.ProvisioningApi.Services;
using Devantler.KindCLI;

namespace Devantler.DataMesh.ProvisioningApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddK8sDataProductProvisioningService(this IServiceCollection services)
    {
        services.AddKindCliService();
        services.AddScoped<IDataProductProvisioningService, K8sDataProductProvisioningService>();
        return services;
    }
}
