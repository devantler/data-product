using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStore;
using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Configuration.Options.TracingExporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;

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
                    _ = builder.AddSqlClientInstrumentation();
                    _ = builder.AddEntityFrameworkCoreInstrumentation();
                }

                if (options.FeatureFlags.EnableCaching && options.CacheStore.Type == CacheStoreType.Redis)
                {
                    _ = builder.AddRedisInstrumentation();
                }

                _ = options.TracingExporter.Type switch
                {
                    TracingExporterType.OpenTelemetry => builder.AddOtlpExporter(
                        opt =>
                        {
                            var openTelemetryOptions = (OpenTelemetryTracingExporterOptions)options.TracingExporter;
                            opt.Endpoint = new Uri(openTelemetryOptions.Endpoint);
                        }
                    ),
                    TracingExporterType.Console => builder.AddConsoleExporter(),
                    _ => throw new NotSupportedException($"Tracing system type '{options.TracingExporter.Type}' is not supported.")
                };
            });
        return services;
    }
}