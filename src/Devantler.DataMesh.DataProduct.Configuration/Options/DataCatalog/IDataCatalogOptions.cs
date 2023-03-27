namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataCatalog;

/// <summary>
/// Data Catalog Options
/// </summary>
public interface IDataCatalogOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data catalog options.
    /// </summary>
    public const string Key = "DataCatalog";
    /// <summary>
    /// The type of data catalog to use.
    /// </summary>
    public DataCatalogType Type { get; set; }
}