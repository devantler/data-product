using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Telemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace Devantler.DataProduct.Features.Telemetry.Logging;

/// <summary>
/// Extensions to register logging to the DI container and configure the web application to use it.
/// </summary>
public static class LoggingStartupExtensions
{
    /// <summary>
    /// Registers logging to the DI container.
    /// </summary>
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder, DataProductOptions options)
    {
        _ = builder.Logging.AddOpenTelemetry(opt =>
        {
            _ = opt.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService($"{options.Name.ToKebabCase()}-{options.Release}"));
            _ = opt.IncludeScopes = true;

            if (options.Telemetry.EnableTracing)
                _ = opt.AttachLogsToActivityEvent();

            _ = options.Telemetry.Type switch
            {
                TelemetryExporterType.OpenTelemetry => opt.AddOtlpExporter(
                    opt =>
                    {
                        var openTelemetryOptions = (OpenTelemetryOptions)options.Telemetry;
                        opt.Endpoint = new Uri(openTelemetryOptions.Endpoint);
                    }
                ),
                TelemetryExporterType.Console => opt.AddConsoleExporter(),
                _ => throw new NotSupportedException($"Logging exporter '{options.Telemetry.Type}' is not supported.")
            };
        });
        return builder;
    }
}