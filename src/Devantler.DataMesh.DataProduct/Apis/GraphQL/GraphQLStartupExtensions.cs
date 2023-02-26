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
    /// <param name="environment"></param>
    public static void AddGraphQL(this IServiceCollection services, IWebHostEnvironment environment) =>
        services.AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = environment.IsDevelopment())
            .AddQueryType<Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

    /// <summary>
    /// Configures the web application to use GraphQL.
    /// </summary>
    /// <param name="app"></param>
    public static void UseGraphQL(this WebApplication app) =>
        app.MapGraphQL();
}
