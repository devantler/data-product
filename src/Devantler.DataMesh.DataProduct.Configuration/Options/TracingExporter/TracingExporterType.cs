namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingExporter;

/// <summary>
/// The type of tracing system to use.
/// </summary>
public enum TracingExporterType
{
    /// <summary>
    /// Writes tracing information to the console.
    /// </summary>
    Console,
    /// <summary>
    /// OpenTelemetry is a vendor-neutral open source project aiming to standardize how you collect, process, and export telemetry data. Can be used with Jaeger, Zipkin, etc.
    /// </summary>
    OpenTelemetry
}
