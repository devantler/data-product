namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors;

/// <summary>
/// Options to configure a Kafka data ingestor for the data product.
/// </summary>
public class KafkaDataIngestorOptions : IDataIngestorOptions
{
    /// <inheritdoc/>
    public DataIngestorType Type { get; set; } = DataIngestorType.Kafka;

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
