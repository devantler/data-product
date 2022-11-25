using Devantler.DataMesh.Core.Extensions;
using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct.Features.GraphQL;

public static partial class GraphQLStartupExtensions
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.IsFeatureEnabled(Constants.GRAPHQL_FEATURE_FLAG)) return services;

        AddFromSourceGenerator(services);

        return services;
    }

    static partial void AddFromSourceGenerator(IServiceCollection services);

    public static WebApplication UseGraphQL(this WebApplication app)
    {
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
