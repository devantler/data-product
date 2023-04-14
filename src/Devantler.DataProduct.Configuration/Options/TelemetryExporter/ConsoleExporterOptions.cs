namespace Devantler.DataProduct.Configuration.Options.TelemetryExporter;

/// <summary>
/// Options to configure the logging exporter for the data product.
/// </summary>
public class ConsoleExporterOptions : TelemetryExporterOptions
{
    /// <inheritdoc />
    public override TelemetryExporterType Type { get; set; } = TelemetryExporterType.Console;
}