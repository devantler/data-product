namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataCatalog;

/// <summary>
/// LinkedIn DataHub Data Catalog Options
/// </summary>
public class DataHubDataCatalogOptions : DataCatalogOptions
{
    /// <inheritdoc />
    public override DataCatalogType Type { get; set; } = DataCatalogType.DataHub;
}