namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingSystem;

/// <summary>
/// Options to configure Zipkin tracing for the data product.
/// </summary>
public class ZipkinTracingSystemOptions : TracingSystemOptions
{
    /// <inheritdoc/>
    public override TracingSystemType Type { get; set; } = TracingSystemType.Zipkin;

    /// <summary>
    /// The host of the Zipkin agent.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// The port of the Zipkin agent.
    /// </summary>
    public int Port { get; set; } = 9411;
}