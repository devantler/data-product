namespace Devantler.DataProduct.Configuration.Options.DataIngestors;
/// <summary>
/// Options to configure a local data ingestor for the data product.
/// </summary>
public class LocalDataIngestorOptions : DataIngestorOptions
{
    /// <inheritdoc/>
    public override DataIngestorType Type { get; set; } = DataIngestorType.Local;
    /// <summary>
    /// The path to the file to read from.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
}
