namespace Devantler.DataMesh.DataProduct.Features.Apis.GraphQL;

public static class GraphQLStartupExtensions
{
    public static void AddGraphQL(this IServiceCollection services, IConfiguration configuration) => 
        services.AddGraphQLServer();

    public static void UseGraphQL(this WebApplication app, IConfiguration configuration) => 
        app.MapGraphQL();
}
