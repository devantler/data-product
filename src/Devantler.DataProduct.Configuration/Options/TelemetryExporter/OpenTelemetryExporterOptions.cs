namespace Devantler.DataProduct.Configuration.Options.TelemetryExporter;

/// <summary>
/// Options to configure the OpenTelemetry exporter for the data product.
/// </summary>
public class OpenTelemetryExporterOptions : TelemetryExporterOptions
{
    /// <inheritdoc />
    public override TelemetryExporterType Type { get; set; } = TelemetryExporterType.OpenTelemetry;

    /// <summary>
    /// The endpoint to send telemetry data to.
    /// </summary>
    public string Endpoint { get; set; } = "http://localhost:4317";
}