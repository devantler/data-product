namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingSystem;

/// <summary>
/// Options for the OpenTelemetry tracing system.
/// </summary>
public class OpenTelemetryTracingSystemOptions : TracingSystemOptions
{
    /// <summary>
    /// The type of the tracing system.
    /// </summary>
    public override TracingSystemType Type { get; set; } = TracingSystemType.OpenTelemetry;

    /// <summary>
    /// The endpoint to send the traces to.
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;
}