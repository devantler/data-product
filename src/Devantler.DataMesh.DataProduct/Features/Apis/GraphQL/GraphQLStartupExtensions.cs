namespace Devantler.DataMesh.DataProduct.Features.Apis.GraphQL;

/// <summary>
/// Extensions for registering GraphQL to the DI container and configuring the web application to use it.
/// </summary>
public static class GraphQlStartupExtensions
{
    /// <summary>
    /// Registers GraphQL to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddGraphQL(this IServiceCollection services, IWebHostEnvironment environment)
    {
        return services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = environment.IsDevelopment())
            .AddQueryType<Query>()
            .AddProjections() // TODO: Gate this behind a setting in the options.Services.Apis.GraphQL.EnableProjections
            .AddFiltering() // TODO: Gate this behind a setting in the options.Services.Apis.GraphQL.EnableFiltering
            .AddSorting() // TODO: Gate this behind a setting in the options.Services.Apis.GraphQL.EnableSorting
            .Services;
    }

    /// <summary>
    /// Configures the web application to use GraphQL.
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication UseGraphQL(this WebApplication app)
    {
        _ = app.MapGraphQL();
        return app;
    }
}
