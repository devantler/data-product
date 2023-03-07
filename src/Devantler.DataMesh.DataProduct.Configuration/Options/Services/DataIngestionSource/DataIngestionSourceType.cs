namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
/// <summary>
/// The type of data ingestion source to use.
/// </summary>
public enum DataIngestionSourceType
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
