namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;

/// <summary>
/// Options to configure a data ingestion source used by the data product.
/// </summary>
public interface IDataIngestionSourceOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the data ingestion sources options.
    /// </summary>
    public const string Key = "DataProduct:Services:DataIngestionSources";

    /// <summary>
    /// The data ingestion source type to use.
    /// </summary>
    public DataIngestionSourceType Type { get; set; }
}
