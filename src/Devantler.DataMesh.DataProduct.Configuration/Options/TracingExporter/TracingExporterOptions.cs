namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingExporter;

/// <summary>
/// Options to configure a tracing system for the data product.
/// </summary>
public class TracingExporterOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the tracing system options.
    /// </summary>
    public const string Key = "TracingExporter";

    /// <summary>
    /// The type of tracing system to use.
    /// </summary>
    public virtual TracingExporterType Type { get; set; }
}