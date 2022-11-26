using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Core.Extensions;
namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;

public static partial class GraphQLStartupExtensions
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.IsFeatureEnabled(Constants.GRAPHQL_FEATURE_FLAG)) return services;

        AddFromSourceGenerator(services);

        return services;
    }

    static partial void AddFromSourceGenerator(IServiceCollection services);

    public static WebApplication UseGraphQL(this WebApplication app, IConfiguration configuration)
    {
        if (!configuration.IsFeatureEnabled(Constants.GRAPHQL_FEATURE_FLAG)) return app;

        app.UseForFeature(Constants.GRAPHQL_FEATURE_FLAG, app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        });

        return app;
    }
}
