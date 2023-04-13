namespace Devantler.DataProduct.Configuration.Options.MetricsExporter;

/// <summary>
/// The supported metrics systems.
/// </summary>
public enum MetricsExporterType
{
    /// <summary>
    /// OpenTelemetry is a vendor-neutral open source project aiming to standardize how you collect, process, and export metrics data. Can be used with Prometheus, etc.
    /// </summary>
    OpenTelemetry,

    /// <summary>
    /// A simple console exporter that writes metrics to the console.
    /// </summary>
    Console
}