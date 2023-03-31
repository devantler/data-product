namespace Devantler.DataMesh.DataProduct.Configuration.Options.MetricsSystem;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class OpenTelemetryMetricsSystemOptions : MetricsSystemOptions
{
    /// <summary>
    /// The type of metrics system to use.
    /// </summary>
    public override MetricsSystemType Type { get; set; } = MetricsSystemType.OpenTelemetry;

    /// <summary>
    /// The endpoint to send metrics to.
    /// </summary>
    public string Endpoint { get; set; } = "http://localhost:9090";
}