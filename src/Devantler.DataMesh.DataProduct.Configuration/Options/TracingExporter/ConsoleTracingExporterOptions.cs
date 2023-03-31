namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingExporter;

/// <summary>
/// Options to configure console tracing for the data product.
/// </summary>
public class ConsoleTracingExporterOptions : TracingExporterOptions
{
    /// <inheritdoc/>
    public override TracingExporterType Type { get; set; } = TracingExporterType.Console;
}