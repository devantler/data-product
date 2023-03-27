namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataCatalog;

/// <summary>
/// LinkedIn DataHub Data Catalog Options
/// </summary>
public class DataHubDataCatalogOptions : IDataCatalogOptions
{
    /// <inheritdoc />
    public DataCatalogType Type { get; set; } = DataCatalogType.DataHub;
}