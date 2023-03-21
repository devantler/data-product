namespace Devantler.DataMesh.DataProduct.Configuration.Options.Dashboard;

/// <summary>
/// Options for an embedded service in the dashboard.
/// </summary>
public class EmbeddedServiceOptions
{
    /// <summary>
    /// The name of the embedded service.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the embedded service.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}