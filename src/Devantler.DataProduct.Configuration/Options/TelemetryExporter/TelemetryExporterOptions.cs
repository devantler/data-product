namespace Devantler.DataProduct.Configuration.Options.TelemetryExporter;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class TelemetryExporterOptions
{
    /// <summary>
    /// A flag to indicate if logging should be enabled for the data product.
    /// </summary>
    public bool EnableLogging { get; set; }

    /// <summary>
    /// A flag to indicate if metrics should be enabled for the data product.
    /// </summary>
    public bool EnableMetrics { get; set; }

    /// <summary>
    /// A flag to indicate if tracing should be enabled for the data product.
    /// </summary>
    public bool EnableTracing { get; set; }

    /// <summary>
    /// A key to indicate the section in the configuration file that contains the telemetry exporter options.
    /// </summary>
    public const string Key = "TelemetryExporter";

    /// <summary>
    /// The type of telemetry exporter to use.
    /// </summary>
    public virtual TelemetryExporterType Type { get; set; }
}