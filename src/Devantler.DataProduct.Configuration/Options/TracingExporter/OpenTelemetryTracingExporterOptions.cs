namespace Devantler.DataProduct.Configuration.Options.TracingExporter;

/// <summary>
/// Options for the OpenTelemetry tracing system.
/// </summary>
public class OpenTelemetryTracingExporterOptions : TracingExporterOptions
{
    /// <summary>
    /// The type of the tracing system.
    /// </summary>
    public override TracingExporterType Type { get; set; } = TracingExporterType.OpenTelemetry;

    /// <summary>
    /// The endpoint to send the traces to.
    /// </summary>
    public string Endpoint { get; set; } = "http://localhost:4317";
}