using DevAntler.DataMesh.ProvisioningService.Interfaces;
using DevAntler.DataMesh.ProvisioningService.Targets.Local;

namespace DevAntler.DataMesh.ProvisioningService.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddProvisioningTargets(this IServiceCollection services)
    {
        services.AddTransient<IProvisioningService, LocalProvisioningService>();
        return services;
    }
}
