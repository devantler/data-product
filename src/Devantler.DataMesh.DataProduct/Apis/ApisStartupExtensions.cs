using Devantler.DataMesh.DataProduct.Apis.REST;

namespace Devantler.DataMesh.DataProduct.Apis;

public static class ApisStartupExtensions
{
    public static IServiceCollection AddApis(this IServiceCollection services)
    {
        services.AddRESTApi();
        return services;
    }
}
