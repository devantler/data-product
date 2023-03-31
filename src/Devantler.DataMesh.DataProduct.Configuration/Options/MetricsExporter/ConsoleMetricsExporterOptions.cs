namespace Devantler.DataMesh.DataProduct.Configuration.Options.MetricsExporter;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class ConsoleMetricsExporterOptions : MetricsExporterOptions
{
    /// <summary>
    /// The type of metrics system to use.
    /// </summary>
    public override MetricsExporterType Type { get; set; } = MetricsExporterType.Console;
}