using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Features.Tracing;

/// <summary>
/// Extensions to register tracing to the DI container and configure the web application to use it.
/// </summary>
public static class TracingStartupExtensions
{
    /// <summary>
    /// Registers tracing to the DI container.
    /// </summary>
    public static IServiceCollection AddTracing(this IServiceCollection services, DataProductOptions options)
        => services;

    /// <summary>
    /// Configures the web application to use tracing.
    /// </summary>
    public static IApplicationBuilder UseTracing(this IApplicationBuilder app, DataProductOptions options)
        => app;
}