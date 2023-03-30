namespace Devantler.DataMesh.DataProduct.Configuration.Options.TracingSystem;

/// <summary>
/// Options to configure Jaeger tracing for the data product.
/// </summary>
public class JaegerTracingSystemOptions : TracingSystemOptions
{
    /// <inheritdoc/>
    public override TracingSystemType Type { get; set; } = TracingSystemType.Jaeger;

    /// <summary>
    /// The host of the Jaeger agent.
    /// </summary>
    public string Host { get; set; } = "localhost";

    /// <summary>
    /// The port of the Jaeger agent.
    /// </summary>
    public int Port { get; set; } = 6831;
}