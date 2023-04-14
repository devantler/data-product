namespace Devantler.DataProduct.Configuration.Options.TelemetryExporter;

/// <summary>
/// The supported logging exporters.
/// </summary>
public enum TelemetryExporterType
{
    /// <summary>
    /// The OpenTelemetry exporter, a vendor-neutral open source project aiming to standardize how you collect, process, and export telemetry data.
    /// </summary>
    OpenTelemetry,

    /// <summary>
    /// A simple console exporter that writes telemetry to the console.
    /// </summary>
    Console
}