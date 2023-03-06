using Devantler.DataMesh.DataProduct.Apis.GraphQL;
using Devantler.DataMesh.DataProduct.Apis.Rest;
using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Apis;

/// <summary>
/// Extensions to registers APIs to the DI container and configure the web application to use them.
/// </summary>
public static class ApisStartupExtensions
{
    /// <summary>
    /// Registers APIs to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddApis(this IServiceCollection services, DataProductOptions options, IWebHostEnvironment environment)
    {
        if (!options.FeatureFlags.EnableApis.Any())
            return services;

        return services
            .AddRestApi(options)
            .AddGraphQL(options, environment);
    }

    /// <summary>
    /// Configures the web application to use APIs.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseApis(this WebApplication app, DataProductOptions options)
    {
        if (!options.FeatureFlags.EnableApis.Any())
            return app;

        return app
            .UseRestApi(options)
            .UseGraphQL(options);
    }
}
