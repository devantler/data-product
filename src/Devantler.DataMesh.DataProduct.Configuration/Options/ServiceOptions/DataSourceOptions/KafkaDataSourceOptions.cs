namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataSourceOptions;

/// <summary>
/// Options to configure a Kafka data source used by the data product.
/// </summary>
public class KafkaDataSourceOptions : DataSourceOptionsBase
{
    /// <inheritdoc/>
    public override DataSourceType Type { get; set; } = DataSourceType.Kafka;
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
