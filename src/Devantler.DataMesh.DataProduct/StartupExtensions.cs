using Devantler.DataMesh.DataProduct.Features.EntityFramework;
using Devantler.DataMesh.DataProduct.Features.GraphQL;
using Devantler.DataMesh.DataProduct.Features.StateStore;

namespace Devantler.DataMesh.DataProduct;

public static class StartupExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFramework(configuration);
        services.AddGraphQL(configuration);
        services.AddStateStore(configuration);

        //services.AddControllers();
        //services.AddSwaggerGen();
        //services.AddEndpointsApiExplorer();

        return services;
    }

    public static void UseDomain(this WebApplication app)
    {
        app.UseGraphQL();
        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
        // app.UseAuthorization();
        //app.MapControllers();
    }
}
