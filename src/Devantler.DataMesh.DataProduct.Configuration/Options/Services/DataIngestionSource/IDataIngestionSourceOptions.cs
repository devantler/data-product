namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;

/// <summary>
/// Options to configure a data ingestion source used by the data product.
/// </summary>
public interface IDataIngestionSourceOptions
{
    /// <summary>
    /// The key for the data ingestion source options.
    /// </summary>
    public const string Key = "DataProduct:Services:DataIngestionSources";
    /// <summary>
    /// The data ingestion source type to use.
    /// </summary>
    public DataIngestionSourceType Type { get; set; }
}
