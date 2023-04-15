using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Features.Telemetry.Logging;
using Devantler.DataProduct.Features.Telemetry.Metrics;
using Devantler.DataProduct.Features.Telemetry.Tracing;

namespace Devantler.DataProduct.Features.Telemetry;

/// <summary>
/// Extensions to register telemetry to the DI container and configure the web application to use it.
/// </summary>
public static class TelemetryStartupExtensions
{
    /// <summary>
    /// Registers tracing to the DI container.
    /// </summary>
    public static WebApplicationBuilder AddTelemetry(this WebApplicationBuilder builder, DataProductOptions options)
    {
        if (options.Telemetry.EnableLogging)
            _ = builder.AddLogging(options);

        if (options.Telemetry.EnableMetrics)
            _ = builder.Services.AddMetrics(options);

        if (options.Telemetry.EnableTracing)
            _ = builder.Services.AddTracing(options);

        return builder;
    }
}