using Confluent.Kafka;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;

/// <summary>
/// A data ingestion source service that ingests data from a Kafka topic.
/// </summary>
public abstract class KafkaDataIngestionSourceService<TSchema> : IDataIngestionSourceService
    where TSchema : class, Schemas.ISchema
{
    readonly IDataStoreService<TSchema> _dataStoreService;
    readonly string _topic;
    readonly IConsumer<Guid, TSchema> _consumer;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaDataIngestionSourceService{TSchema}"/> class.
    /// </summary>
    protected KafkaDataIngestionSourceService(IDataStoreService<TSchema> dataStoreService, IOptions<KafkaDataIngestionSourceOptions> options)
    {
        _dataStoreService = dataStoreService;
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            GroupId = options.Value.GroupId
        };
        _topic = options.Value.Topic;
        _consumer = new ConsumerBuilder<Guid, TSchema>(consumerConfig).Build();
    }

    /// <summary>
    /// Starts the kafka data ingestion source service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_topic);
        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = _consumer.Consume(cancellationToken);
            var schema = consumeResult.Message.Value;
            schema.Id = consumeResult.Message.Key;
            _ = _dataStoreService.CreateSingleAsync(schema, cancellationToken);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops the kafka data ingestion source service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return Task.CompletedTask;
    }
}
