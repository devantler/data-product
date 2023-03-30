namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingSystem;

/// <summary>
/// Options to configure console tracing for the data product.
/// </summary>
public class ConsoleTracingSystemOptions : TracingSystemOptions
{
    /// <inheritdoc/>
    public override TracingSystemType Type { get; set; } = TracingSystemType.Console;
}