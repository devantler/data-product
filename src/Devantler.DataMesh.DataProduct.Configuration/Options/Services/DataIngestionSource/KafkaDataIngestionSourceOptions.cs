namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;

/// <summary>
/// Options to configure a Kafka data ingestion source used by the data product.
/// </summary>
public class KafkaDataIngestionSourceOptions : IDataIngestionSourceOptions
{
    /// <inheritdoc/>
    public DataIngestionSourceType Type { get; set; } = DataIngestionSourceType.Kafka;
    /// <summary>
    /// The BootstrapServers to connect to.
    /// </summary>
    public string BootstrapServers { get; set; } = string.Empty;
    /// <summary>
    /// The topic to read from.
    /// </summary>
    public string Topic { get; set; } = string.Empty;
    /// <summary>
    /// The consumer group id to use.
    /// </summary>
    public string GroupId { get; set; } = string.Empty;
}
