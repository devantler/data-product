using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Core.Configuration.Options;
using Devantler.DataProduct.Core.Configuration.Options.CacheStore;
using Devantler.DataProduct.Core.Configuration.Options.DataStore;
using Devantler.DataProduct.Core.Configuration.Options.FeatureFlags;
using Devantler.DataProduct.Core.Configuration.Options.Telemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Devantler.DataProduct.Features.Telemetry.Tracing;

/// <summary>
/// Extensions to register tracing to the DI container and configure the web application to use it.
/// </summary>
public static class TracingStartupExtensions
{
    /// <summary>
    /// Registers tracing to the DI container.
    /// </summary>
    public static IServiceCollection AddTracing(this IServiceCollection services, DataProductOptions options)
    {
        _ = services.AddOpenTelemetry()
            .WithTracing(builder =>
            {
                _ = builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{options.Name.ToKebabCase()}-{options.Release}"));

                _ = builder.AddAspNetCoreInstrumentation();
                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.Rest) || options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
                    _ = builder.AddHttpClientInstrumentation();

                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.gRPC))
                    _ = builder.AddGrpcClientInstrumentation();

                if (options.DataStore.Type == DataStoreType.SQL)
                {
                    _ = builder.AddEntityFrameworkCoreInstrumentation();
                }

                if (options.FeatureFlags.EnableCaching && options.CacheStore.Type == CacheStoreType.Redis)
                {
                    _ = builder.AddRedisInstrumentation();
                }

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
                    _ => throw new NotSupportedException($"Tracing system type '{options.Telemetry.ExporterType}' is not supported.")
                };
            });
        return services;
    }
}