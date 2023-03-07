using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.Apis.GraphQL;
using Devantler.DataMesh.DataProduct.Features.Apis.Rest;

namespace Devantler.DataMesh.DataProduct.Features.Apis;

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
        return options.FeatureFlags.EnableApis.Any()
            ? services
                .AddRest(options)
                .AddGraphQL(environment)
            : services;
    }

    /// <summary>
    /// Configures the web application to use APIs.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseApis(this WebApplication app, DataProductOptions options)
    {
        return options.FeatureFlags.EnableApis.Any()
            ? app
                .UseRest(options)
                .UseGraphQL()
            : app;
    }
}
