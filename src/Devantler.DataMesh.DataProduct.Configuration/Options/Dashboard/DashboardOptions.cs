namespace Devantler.DataMesh.DataProduct.Configuration.Options.Dashboard;

/// <summary>
/// Options for the dashboard.
/// </summary> 
public class DashboardOptions
{
    /// <summary>
    /// A set of urls that should be added to the Content Security Policy (CSP) directives to allow cross-origin requests in the embedded iframes.
    /// </summary>
    /// <remarks>
    /// This setting is only really useful if you are embedding a service that redirects to different domains. For example if you are embedding a service that uses OAuth2 and redirects to the OAuth2 provider.
    /// </remarks>
    public List<string> CSPFrameAncestors { get; set; } = new();

    /// <summary>
    /// The embedded services in the dashboard.
    /// </summary>
    public List<EmbeddedServiceOptions> EmbeddedServices { get; set; } = new();
}