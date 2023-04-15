namespace Devantler.DataProduct.Core.Configuration.Options.DataCatalog;

/// <summary>
/// Data Catalog Options
/// </summary>
public class DataCatalogOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data catalog options.
    /// </summary>
    public const string Key = "DataCatalog";
    /// <summary>
    /// The type of data catalog to use.
    /// </summary>
    public virtual DataCatalogType Type { get; set; }

    /// <summary>
    /// The URL of the data catalog.
    /// </summary>
    public virtual string Url { get; set; } = string.Empty;

    /// <summary>
    /// The API Access Token
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
}