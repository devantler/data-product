namespace Devantler.DataProduct.Core.Configuration.Options.Telemetry;

/// <summary>
/// Options to configure console telemetry for the data product.
/// </summary>
public class ConsoleTelemetryOptions : TelemetryOptions
{
    /// <inheritdoc />
    public override TelemetryExporterType Type { get; set; } = TelemetryExporterType.Console;
}