namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors;
/// <summary>
/// Options to configure a local data ingestor for the data product.
/// </summary>
public class LocalDataIngestorOptions : IDataIngestorOptions
{
    /// <inheritdoc/>
    public string Name { get; set; } = "Local";

    /// <inheritdoc/>
    public DataIngestorType Type { get; set; } = DataIngestorType.Local;
    /// <summary>
    /// The path to the file to read from.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
}
