namespace Devantler.DataProduct.Configuration.Options.DataIngestors;

/// <summary>
/// Options to configure a data ingestor for the data product.
/// </summary>
public class DataIngestorOptions
{
    /// <summary>
    /// The key for the data ingestor options.
    /// </summary>
    public const string Key = "DataIngestors";

    /// <summary>
    /// The data ingestor type to use.
    /// </summary>
    public virtual DataIngestorType Type { get; set; }
}
