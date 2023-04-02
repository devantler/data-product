namespace Devantler.DataMesh.DataProduct.Configuration.Options.LoggingExporter;

/// <summary>
/// Options to configure the metrics system for the data product.
/// </summary>
public class LoggingExporterOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the logging exporter options.
    /// </summary>
    public const string Key = "LoggingExporter";

    /// <summary>
    /// The type of logging system to use.
    /// </summary>
    public virtual LoggingExporterType Type { get; set; }
}