namespace Devantler.DataMesh.DataProduct.Configuration.Options.LoggingExporter;

/// <summary>
/// The supported logging exporters.
/// </summary>
public enum LoggingExporterType
{
    /// <summary>
    /// OpenTelemetry is a vendor-neutral open source project aiming to standardize how you collect, process, and export logs. Can be used with Prometheus, etc.
    /// </summary>
    OpenTelemetry,

    /// <summary>
    /// A simple console exporter that writes logs to the console.
    /// </summary>
    Console
}