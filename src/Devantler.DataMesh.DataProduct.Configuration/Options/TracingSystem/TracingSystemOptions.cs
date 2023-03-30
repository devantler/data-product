namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingSystem;

/// <summary>
/// Options to configure a tracing system for the data product.
/// </summary>
public class TracingSystemOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the tracing system options.
    /// </summary>
    public const string Key = "TracingSystem";

    /// <summary>
    /// The type of tracing system to use.
    /// </summary>
    public virtual TracingSystemType Type { get; set; }
}