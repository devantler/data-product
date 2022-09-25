using DevAntler.DataMesh.DataProductService.Interfaces;
using DevAntler.DataMesh.DataProductService.Targets.Local;

namespace DevAntler.DataMesh.DataProductService.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddProvisioningTargets(this IServiceCollection services)
    {
        services.AddTransient<IProvisioningService, LocalProvisioningService>();
        return services;
    }
}
