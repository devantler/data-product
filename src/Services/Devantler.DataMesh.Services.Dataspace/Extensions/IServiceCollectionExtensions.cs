using Devantler.DataMesh.Services.Dataspace.Provisioning;
using Devantler.DataMesh.Services.Dataspace.Provisioning.Targets.Local;

namespace Devantler.DataMesh.Services.Dataspace.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddProvisioningTargets(this IServiceCollection services)
    {
        services.AddTransient<IProvisioningService, LocalProvisioningService>();
        return services;
    }
}
