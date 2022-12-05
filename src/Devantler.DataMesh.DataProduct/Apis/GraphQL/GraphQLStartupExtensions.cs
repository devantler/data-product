namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;

public static class GraphQLStartupExtensions
{
    public static void AddGraphQL(this IServiceCollection services) =>
        services.AddGraphQLServer();

    public static void UseGraphQL(this WebApplication app) =>
        app.MapGraphQL();
}
