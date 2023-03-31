using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Configuration.Options.MetricsSystem;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

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
    {
        _ = services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                _ = builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{options.Name.ToKebabCase()}-{options.Release}"));
                _ = builder.AddAspNetCoreInstrumentation();
                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.Rest) || options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
                    _ = builder.AddHttpClientInstrumentation();

                _ = options.MetricsSystem.Type switch
                {
                    MetricsSystemType.Prometheus => builder.AddPrometheusExporter(),
                    MetricsSystemType.Console => builder.AddConsoleExporter(),
                    _ => throw new NotSupportedException($"Metrics system type '{options.MetricsSystem.Type}' is not supported."),
                };
            });
        return services;
    }
}