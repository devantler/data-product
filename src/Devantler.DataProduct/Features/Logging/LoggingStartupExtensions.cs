using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.LoggingExporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace Devantler.DataProduct.Features.Logging;

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
            _ = opt.AttachLogsToActivityEvent();
            _ = options.LoggingExporter.Type switch
            {
                LoggingExporterType.OpenTelemetry => opt.AddOtlpExporter(
                    opt =>
                    {
                        var openTelemetryOptions = (OpenTelemetryLoggingExporterOptions)options.LoggingExporter;
                        opt.Endpoint = new Uri(openTelemetryOptions.Endpoint);
                    }
                ),
                LoggingExporterType.Console => opt.AddConsoleExporter(),
                _ => throw new NotSupportedException($"Logging exporter '{options.LoggingExporter.Type}' is not supported.")
            };
        });
        return builder;
    }
}