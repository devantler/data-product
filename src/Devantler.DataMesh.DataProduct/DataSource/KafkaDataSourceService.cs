using Devantler.DataMesh.DataProduct.Configuration.Options.DataSourceOptions;
using Devantler.DataMesh.DataProduct.Ingestion;

/// <summary>
/// A data source service that ingests data from a Kafka topic.
/// </summary>
public class KafkaDataSourceService : IDataSourceService
{
    private readonly string topic;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaDataSourceService"/> class.
    /// </summary>
    public KafkaDataSourceService(KafkaDataSourceOptions options)
    {
        this.topic = options.Topic;
    }

    /// <summary>
    /// Starts the kafka data source service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task StartAsync(CancellationToken cancellationToken) => throw new NotImplementedException();

    /// <summary>
    /// Stops the kafka data source service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task StopAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}
