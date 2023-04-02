namespace Devantler.DataMesh.DataProduct.Configuration.Options.LoggingExporter;

/// <summary>
/// Options to configure the logging exporter for the data product.
/// </summary>
public class ConsoleLoggingExporterOptions : LoggingExporterOptions
{
    /// <summary>
    /// The type of logging exporter to use.
    /// </summary>
    public override LoggingExporterType Type { get; set; } = LoggingExporterType.Console;
}