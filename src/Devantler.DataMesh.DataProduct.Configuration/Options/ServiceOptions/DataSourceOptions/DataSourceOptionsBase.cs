namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataSourceOptions;

/// <summary>
/// Options to configure a data source used by the data product.
/// </summary>
public abstract class DataSourceOptionsBase : IDataSourceOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data sources options.
    /// </summary>
    public const string Key = "DataProduct:Services:DataSources";

    /// <inheritdoc/>
    public abstract DataSourceType Type { get; set; }
}
