namespace Devantler.DataMesh.DataProduct.Configuration.Options.MetricsSystem;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class MetricsSystemOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the metrics system options.
    /// </summary>
    public const string Key = "MetricsSystem";

    /// <summary>
    /// The type of metrics system to use.
    /// </summary>
    public virtual MetricsSystemType Type { get; set; }
}