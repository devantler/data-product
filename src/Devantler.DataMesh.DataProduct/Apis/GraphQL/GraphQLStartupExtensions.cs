namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;

public static class GraphQlStartupExtensions
{
    public static void AddGraphQl(this IServiceCollection services) =>
        services.AddGraphQLServer();

    public static void UseGraphQl(this WebApplication app) =>
        app.MapGraphQL();
}
