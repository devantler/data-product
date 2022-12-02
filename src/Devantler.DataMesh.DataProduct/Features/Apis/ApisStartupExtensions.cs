using Devantler.DataMesh.DataProduct.Features.Apis.GraphQL;
using Devantler.DataMesh.DataProduct.Features.Apis.REST;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.Features.Apis;

public static class ApisStartupExtensions
{
    public static void AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(REST.Constants.REST_FEATURE_FLAG, REST.Constants.REST_FEATURE_FLAG_VALUE))
            services.AddRESTApi(configuration);
        if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GRAPHQL_FEATURE_FLAG, GraphQL.Constants.GRAPHQL_FEATURE_FLAG_VALUE))
            services.AddGraphQL(configuration);
    }

    public static void UseApis(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(REST.Constants.REST_FEATURE_FLAG, REST.Constants.REST_FEATURE_FLAG_VALUE))
            app.UseRESTApi(configuration);
        if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GRAPHQL_FEATURE_FLAG, GraphQL.Constants.GRAPHQL_FEATURE_FLAG_VALUE))
            app.UseGraphQL(configuration);
    }
}
