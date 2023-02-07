namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;

/// <summary>
/// Extensions for registering GraphQL to the DI container and configuring the web application to use it.
/// </summary>
public static class GraphQLStartupExtensions
{
    /// <summary>
    /// Registers GraphQL to the DI container.
    /// </summary>
    /// <param name="services"></param>
    public static void AddGraphQL(this IServiceCollection services) =>
        services.AddGraphQLServer();

    /// <summary>
    /// Configures the web application to use GraphQL.
    /// </summary>
    /// <param name="app"></param>
    public static void UseGraphQL(this WebApplication app) =>
        app.MapGraphQL();
}
