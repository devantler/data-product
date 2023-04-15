namespace Devantler.DataProduct.Core.Configuration.Options.Dashboard;

/// <summary>
/// Options for links in the dashboard.
/// </summary>
public class LinksOptions
{
    /// <summary>
    /// The name of the link.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The url of the link.
    /// </summary>
    public string Url { get; set; } = string.Empty;
}