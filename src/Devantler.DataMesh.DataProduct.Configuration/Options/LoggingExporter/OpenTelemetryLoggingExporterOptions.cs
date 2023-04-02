namespace Devantler.DataMesh.DataProduct.Configuration.Options.LoggingExporter;

/// <summary>
/// Options to configure the logging exporter for the data product.
/// </summary> 
public class OpenTelemetryLoggingExporterOptions : LoggingExporterOptions
{
    /// <summary>
    /// The type of logging exporter to use.
    /// </summary>
    public override LoggingExporterType Type { get; set; } = LoggingExporterType.OpenTelemetry;

    /// <summary>
    /// The endpoint to send logs to.
    /// </summary>
    public string Endpoint { get; set; } = "http://localhost:4317";
}