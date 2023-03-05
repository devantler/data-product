using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions;

namespace Devantler.DataMesh.DataProduct.Configuration.Options.DataSourceOptions;

/// <summary>
/// Options to configure a Kafka data source used by the data product.
/// </summary>
public class KafkaDataSourceOptions : DataSourceOptionsBase
{
    /// <summary>
    /// The URL to the Kafka broker.
    /// </summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>
    /// The topic to read from.
    /// </summary>
    public string Topic { get; set; } = string.Empty;
    /// <inheritdoc/>
    public override DataSourceType Type { get; set; } = DataSourceType.Kafka;
}
