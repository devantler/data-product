namespace Devantler.DataProduct.Configuration.Options.MetricsExporter;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class MetricsExporterOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the metrics system options.
    /// </summary>
    public const string Key = "MetricsExporter";

    /// <summary>
    /// The type of metrics system to use.
    /// </summary>
    public virtual MetricsExporterType Type { get; set; }
}