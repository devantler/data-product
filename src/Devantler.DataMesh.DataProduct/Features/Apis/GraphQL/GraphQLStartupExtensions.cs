using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Features.Apis.GraphQL;

/// <summary>
/// Extensions for registering GraphQL to the DI container and configuring the web application to use it.
/// </summary>
public static class GraphQLStartupExtensions
{
    /// <summary>
    /// Registers GraphQL to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddGraphQL(this IServiceCollection services, DataProductOptions options,
        IWebHostEnvironment environment)
    {
        var requestExecutorBuilder = services.AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = environment.IsDevelopment())
            .AddQueryType<Query>();
        if (options.Services.Apis.GraphQL.EnableProjections)
            _ = requestExecutorBuilder.AddProjections();
        if (options.Services.Apis.GraphQL.EnableFiltering)
            _ = requestExecutorBuilder.AddFiltering();
        if (options.Services.Apis.GraphQL.EnableSorting)
            _ = requestExecutorBuilder.AddSorting();

        return services;
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
