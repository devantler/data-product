using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStore;
using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Configuration.Options.TracingSystem;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
                    _ = builder.AddSqlClientInstrumentation();

                _ = options.TracingSystem.Type switch
                {
                    TracingSystemType.Jaeger => builder.AddJaegerExporter(
                        opt =>
                        {
                            var jaegerOptions = (JaegerTracingSystemOptions)options.TracingSystem;
                            opt.AgentHost = jaegerOptions.Host;
                            opt.AgentPort = jaegerOptions.Port;
                        }
                    ),
                    TracingSystemType.Zipkin => builder.AddZipkinExporter(),
                    TracingSystemType.Console => builder.AddConsoleExporter(),
                    _ => builder.AddConsoleExporter(),
                };
            });
        return services;
    }
}