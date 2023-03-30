namespace Devantler.DataMesh.DataProduct.Configuration.Options.MetricsSystem;

/// <summary>
/// The supported metrics systems.
/// </summary>
public enum MetricsSystemType
{
    /// <summary>
    /// An open-source monitoring and alerting toolkit used for collecting and analyzing metrics from various systems and applications.
    /// </summary>
    Prometheus,

    /// <summary>
    /// A simple console exporter that writes metrics to the console.
    /// </summary>
    Console,
}