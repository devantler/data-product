using Devantler.DataMesh.DataProduct.Apis.Rest;

namespace Devantler.DataMesh.DataProduct.Apis;

/// <summary>
/// Extensions to registers APIs to the DI container and configure the web application to use them.
/// </summary>
public static class ApisStartupExtensions
{
    /// <summary>
    /// Registers APIs to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(Constants.RestFeatureFlag,
                Constants.RestFeatureFlagValue))
        {
            services.AddRestApi();
        }
        if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GraphQLFeatureFlag,
                GraphQL.Constants.GraphQLFeatureFlagValue))
        {
            _ = services.AddGraphQL();
        }
    }

    /// <summary>
    /// Configures the web application to use APIs.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
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
