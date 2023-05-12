using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataProduct.Configuration.Options.Telemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Devantler.DataProduct.Features.Telemetry.Metrics;

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
                _ = builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(options.Name.ToKebabCase())
                    .AddAttributes(TelemetryHelpers.GetProcessAttributes(options))
                );

                _ = builder.AddAspNetCoreInstrumentation();
                _ = builder.AddRuntimeInstrumentation();
                _ = builder.AddProcessInstrumentation();

                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.Rest) || options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
                    _ = builder.AddHttpClientInstrumentation();

                builder.AddMetricsExporter(options);
            });
        return services;
    }

    static void AddMetricsExporter(this MeterProviderBuilder builder, DataProductOptions options)
    {
        _ = options.Telemetry.ExporterType switch
        {
            TelemetryExporterType.OpenTelemetry => builder.AddOtlpExporter(
                opt =>
                {
                    var openTelemetryOptions = (OpenTelemetryOptions)options.Telemetry;
                    opt.Endpoint = new Uri(openTelemetryOptions.Endpoint);
                }
            ),
            TelemetryExporterType.Console => builder.AddConsoleExporter(),
            _ => throw new NotSupportedException($"Metrics system type '{options.Telemetry.ExporterType}' is not supported.")
        };
    }
}