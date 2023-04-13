using Devantler.Commons.StringHelpers;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataProduct.Configuration.Options.MetricsExporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Devantler.DataProduct.Features.Metrics;

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
                _ = builder.AddRuntimeInstrumentation();
                _ = builder.AddProcessInstrumentation();

                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.Rest) || options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
                    _ = builder.AddHttpClientInstrumentation();

                _ = options.MetricsExporter.Type switch
                {
                    MetricsExporterType.OpenTelemetry => builder.AddOtlpExporter(
                        opt =>
                        {
                            var openTelemetryOptions = (OpenTelemetryMetricsExporterOptions)options.MetricsExporter;
                            opt.Endpoint = new Uri(openTelemetryOptions.Endpoint);
                        }
                    ),
                    MetricsExporterType.Console => builder.AddConsoleExporter(),
                    _ => throw new NotSupportedException($"Metrics system type '{options.MetricsExporter.Type}' is not supported.")
                };
            });
        return services;
    }
}