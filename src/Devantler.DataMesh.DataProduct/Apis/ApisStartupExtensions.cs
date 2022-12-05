using Devantler.DataMesh.DataProduct.Extensions;
using Devantler.DataMesh.DataProduct.Apis.GraphQL;
using Devantler.DataMesh.DataProduct.Apis.Rest;

namespace Devantler.DataMesh.DataProduct.Apis;

public static class ApisStartupExtensions
{
    public static void AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(Rest.Constants.REST_FEATURE_FLAG, Rest.Constants.REST_FEATURE_FLAG_VALUE))
            services.AddRestApi();
        if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GRAPHQL_FEATURE_FLAG, GraphQL.Constants.GRAPHQL_FEATURE_FLAG_VALUE))
            services.AddGraphQL();
    }

    public static void UseApis(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled<string[]>(Rest.Constants.REST_FEATURE_FLAG, Rest.Constants.REST_FEATURE_FLAG_VALUE))
            app.UseRestApi(configuration);
        if (configuration.IsFeatureEnabled<string[]>(GraphQL.Constants.GRAPHQL_FEATURE_FLAG, GraphQL.Constants.GRAPHQL_FEATURE_FLAG_VALUE))
            app.UseGraphQL();
    }
}
