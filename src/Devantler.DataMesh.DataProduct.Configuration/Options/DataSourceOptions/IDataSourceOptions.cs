namespace Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions;

/// <summary>
/// Options to configure a data source used by the data product.
/// </summary>
public interface IDataSourceOptions
{
    /// <summary>
    /// The data source type to use.
    /// </summary>
    public DataSourceType Type { get; set; }
}
