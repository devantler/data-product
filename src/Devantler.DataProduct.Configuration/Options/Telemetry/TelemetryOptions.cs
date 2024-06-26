namespace Devantler.DataProduct.Configuration.Options.Telemetry;

/// <summary>
/// Options to configure telemetry for the data product.
/// </summary>
public class TelemetryOptions
{
  /// <summary>
  /// A flag to indicate if logging should be enabled for the data product.
  /// </summary>
  public bool EnableLogging { get; set; } = true;

  /// <summary>
  /// A flag to indicate if metrics should be enabled for the data product.
  /// </summary>
  public bool EnableMetrics { get; set; } = true;

  /// <summary>
  /// A flag to indicate if tracing should be enabled for the data product.
  /// </summary>
  public bool EnableTracing { get; set; } = true;

  /// <summary>
  /// A key to indicate the section in the configuration file that contains the telemetry options.
  /// </summary>
  public const string Key = "Telemetry";

  /// <summary>
  /// The type of telemetry exporter to use.
  /// </summary>
  public virtual TelemetryExporterType ExporterType { get; set; }
}
