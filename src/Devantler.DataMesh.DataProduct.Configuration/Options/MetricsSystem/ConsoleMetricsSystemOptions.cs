namespace Devantler.DataMesh.DataProduct.Configuration.Options.MetricsSystem;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class ConsoleMetricsSystemOptions : MetricsSystemOptions
{
    /// <summary>
    /// The type of metrics system to use.
    /// </summary>
    public override MetricsSystemType Type { get; set; } = MetricsSystemType.Console;
}