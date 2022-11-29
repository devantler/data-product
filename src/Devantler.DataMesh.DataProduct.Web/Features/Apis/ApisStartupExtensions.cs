using Devantler.DataMesh.DataProduct.Web.Extensions;
using Devantler.DataMesh.DataProduct.Web.Features.Apis.REST;

namespace Devantler.DataMesh.DataProduct.Web.Features.Apis;

public static class ApisStartupExtensions
{
    public static IServiceCollection AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.REST_FEATURE_FLAG))
            services.AddRESTApi();

        return services;
    }
}
