using Devantler.DataMesh.DataProduct.Core.Extensions;

namespace Devantler.DataMesh.DataProduct.Storage.StateStore;

public static partial class StateStoreStartupExtensions
{
    public static IServiceCollection AddStateStore(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.STATE_STORE_FEATURE_FLAG)) return services;

        services.AddDaprClient();

        AddFromSourceGenerator(services);

        return services;
    }

    static partial void AddFromSourceGenerator(IServiceCollection services);
}
