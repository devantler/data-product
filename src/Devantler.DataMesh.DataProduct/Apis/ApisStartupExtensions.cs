using Devantler.DataMesh.DataProduct.Apis.REST;
using Devantler.DataMesh.DataProduct.Core.Extensions;

namespace Devantler.DataMesh.DataProduct.Apis;

public static class ApisStartupExtensions
{
    public static IServiceCollection AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.REST_FEATURE_FLAG))
            services.AddRESTApi();

        return services;
    }
}
