namespace Devantler.DataProduct.Configuration.Options.DataIngestors;
/// <summary>
/// Different data ingestor types.
/// </summary>
public enum DataIngestorType
{
    /// <summary>
    /// A local file data ingestion source.
    /// </summary>
    Local,
    /// <summary>
    /// A kafka data ingestion source.
    /// </summary>
    Kafka
}
