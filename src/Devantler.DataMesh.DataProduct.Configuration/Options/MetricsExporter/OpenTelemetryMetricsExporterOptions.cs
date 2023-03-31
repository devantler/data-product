namespace Devantler.DataMesh.DataProduct.Configuration.Options.MetricsExporter;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class OpenTelemetryMetricsExporterOptions : MetricsExporterOptions
{
    /// <summary>
    /// The type of metrics system to use.
    /// </summary>
    public override MetricsExporterType Type { get; set; } = MetricsExporterType.OpenTelemetry;

    /// <summary>
    /// The endpoint to send metrics to.
    /// </summary>
    public string Endpoint { get; set; } = "http://localhost:4317";
}