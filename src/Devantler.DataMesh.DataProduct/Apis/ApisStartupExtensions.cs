using Devantler.DataMesh.DataProduct.Apis.Rest;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.Apis;

public static class ApisStartupExtensions
{
    public static void AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(Constants.RestFeatureFlag,
                Constants.RestFeatureFlagValue))
        {
            services.AddRestApi();
        }
        if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GraphQlFeatureFlag,
                GraphQL.Constants.GraphQlFeatureFlagValue))
        {
            _ = services.AddGraphQL();
        }
    }

    public static void UseApis(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(Constants.RestFeatureFlag,
                Constants.RestFeatureFlagValue))
        {
            app.UseRestApi(configuration);
        }
        //if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GRAPHQL_FEATURE_FLAG,
        //GraphQL.Constants.GRAPHQL_FEATURE_FLAG_VALUE))
        //app.UseGraphQL();
    }
}
