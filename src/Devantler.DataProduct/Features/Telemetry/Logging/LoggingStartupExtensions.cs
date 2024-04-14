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
    _ = builder.Logging.AddOpenTelemetry(loggingBuilder =>
    {
      _ = loggingBuilder.SetResourceBuilder(ResourceBuilder.CreateDefault()
              .AddService(options.Name.ToKebabCase())
              .AddAttributes(TelemetryHelpers.GetProcessAttributes(options))
          );
      loggingBuilder.IncludeFormattedMessage = true;
      loggingBuilder.IncludeScopes = true;
      loggingBuilder.ParseStateValues = true;

      loggingBuilder.AddLoggingExporter(options);
    });
    return builder;
  }

  static void AddLoggingExporter(this OpenTelemetryLoggerOptions builder, DataProductOptions options)
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
      _ => throw new NotSupportedException($"Logging exporter '{options.Telemetry.ExporterType}' is not supported.")
    };
  }
}
