using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;

/// <summary>
/// Extensions for registering GraphQL to the DI container and configuring the web application to use it.
/// </summary>
public static class GraphQlStartupExtensions
{
    /// <summary>
    /// Registers GraphQL to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddGraphQL(this IServiceCollection services, DataProductOptions options, IWebHostEnvironment environment)
    {
        return services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = environment.IsDevelopment())
            .AddQueryType<Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .Services;
    }

    /// <summary>
    /// Configures the web application to use GraphQL.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseGraphQL(this WebApplication app, DataProductOptions options)
    {
        app.MapGraphQL();
        return app;
    }
}
