using Devantler.DataMesh.DataProduct.Apis.GraphQL;
using Devantler.DataMesh.DataProduct.Apis.Rest;
using Devantler.DataMesh.DataProduct.Configuration.Options;

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
    /// <param name="options"></param>
    public static void AddApis(this IServiceCollection services, DataProductOptions options)
    {
        if (options.FeatureFlags.EnableApis.Contains(Rest.Constants.RestFeatureFlagValue))
        {
            services.AddRestApi();
        }
        if (options.FeatureFlags.EnableApis.Contains(GraphQL.Constants.GraphQLFeatureFlagValue))
        {
            services.AddGraphQL();
        }
    }

    /// <summary>
    /// Configures the web application to use APIs.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static void UseApis(this WebApplication app, DataProductOptions options)
    {
        if (options.FeatureFlags.EnableApis.Contains(Rest.Constants.RestFeatureFlagValue))
        {
            app.UseRestApi(options.FeatureFlags);
        }
        //if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GRAPHQL_FEATURE_FLAG,
        //GraphQL.Constants.GRAPHQL_FEATURE_FLAG_VALUE))
        //app.UseGraphQL();
    }
}
