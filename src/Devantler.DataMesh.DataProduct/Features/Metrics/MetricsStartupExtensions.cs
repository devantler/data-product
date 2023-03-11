using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Features.Metrics;

/// <summary>
/// Extensions to register metrics to the DI container and configure the web application to use it.
/// </summary>
public static class MetricsStartupExtensions
{
    /// <summary>
    /// Registers metrics to the DI container.
    /// </summary>
    public static IServiceCollection AddMetrics(this IServiceCollection services, DataProductOptions options)
        => services;

    /// <summary>
    /// Configures the web application to use metrics.
    /// </summary>
    public static IApplicationBuilder UseMetrics(this IApplicationBuilder app, DataProductOptions options)
        => app;
}