namespace Devantler.DataProduct.Configuration.Options.Telemetry;

/// <summary>
/// Options to configure the OpenTelemetry for the data product.
/// </summary>
public class OpenTelemetryOptions : TelemetryOptions
{
  /// <inheritdoc />
  public override TelemetryExporterType ExporterType { get; set; } = TelemetryExporterType.OpenTelemetry;

  /// <summary>
  /// The endpoint to send telemetry data to.
  /// </summary>
  public string Endpoint { get; set; } = "http://localhost:4317";
}
