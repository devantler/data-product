
using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataEgestion;

/// <summary>
/// Extensions to registers data egestion to the DI container and configure the web application to use it.
/// </summary>
public static class DataEgestionStartupExtensions
{
    /// <summary>
    /// Registers data egestion to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddDataEgestion(this IServiceCollection services, DataProductOptions options)
        => services;


    /// <summary>
    /// Configures the web application to use data egestion.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static IApplicationBuilder UseDataEgestion(this IApplicationBuilder app, DataProductOptions options)
        => app;
}