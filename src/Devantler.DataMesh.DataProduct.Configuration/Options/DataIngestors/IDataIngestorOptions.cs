namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors;

/// <summary>
/// Options to configure a data ingestor for the data product.
/// </summary>
public interface IDataIngestorOptions
{
    /// <summary>
    /// The key for the data ingestor options.
    /// </summary>
    public const string Key = "DataIngestors";

    /// <summary>
    /// The name of the data ingestor.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The data ingestor type to use.
    /// </summary>
    public DataIngestorType Type { get; set; }
}
