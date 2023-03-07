namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
/// <summary>
/// Options to configure a local data ingestion source used by the data product.
/// </summary>
public class LocalDataIngestionSourceOptions : IDataIngestionSourceOptions
{
    /// <inheritdoc/>
    public DataIngestionSourceType Type { get; set; } = DataIngestionSourceType.Local;
    /// <summary>
    /// The path to the file to read from.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
}
