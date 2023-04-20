using System.Reflection;
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
                _ = builder
                    .AddSource(options.Name.ToKebabCase())
                    .SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(options.Name.ToKebabCase())
                        .AddAttributes(
                            new Dictionary<string, object>
                            {
                                ["environment"] = options.Environment,
                                ["service"] = options.Name.ToKebabCase(),
                                ["version"] = options.Release,
                                ["assembly"] = Assembly.GetExecutingAssembly().GetName().FullName
                            }
                        )
                );

                _ = builder.AddAspNetCoreInstrumentation(options => options.RecordException = true);
                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.Rest) || options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.GraphQL))
                    _ = builder.AddHttpClientInstrumentation(options => options.RecordException = true);

                if (options.FeatureFlags.EnableApis.Contains(ApiFeatureFlagValues.gRPC))
                    _ = builder.AddGrpcClientInstrumentation();

                if (options.DataStore.Type == DataStoreType.SQL)
                {
                    _ = builder.AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true);
                }

                if (options.FeatureFlags.EnableCaching && options.CacheStore.Type == CacheStoreType.Redis)
                {
                    _ = builder.AddRedisInstrumentation(configure: config => config.SetVerboseDatabaseStatements = true);
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