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
    public static void AddGraphQl(this IServiceCollection services) =>
        services.AddGraphQLServer();

    /// <summary>
    /// Configures the web application to use GraphQL.
    /// </summary>
    /// <param name="app"></param>
    public static void UseGraphQl(this WebApplication app) =>
        app.MapGraphQL();
}
