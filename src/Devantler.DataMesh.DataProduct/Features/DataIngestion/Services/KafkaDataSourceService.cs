using Confluent.Kafka;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Devantler.DataMesh.DataProduct.Models;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestionSources.Services;

/// <summary>
/// A data ingestion source service that ingests data from a Kafka topic.
/// </summary>
public abstract class KafkaDataIngestionSourceService<TModel> : IDataIngestionSourceService
    where TModel : class, IModel
{
    readonly IDataStoreService<TModel> _dataStoreService;
    readonly string _topic;
    readonly IConsumer<Guid, TModel> _consumer;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaDataIngestionSourceService{TModel}"/> class.
    /// </summary>
    protected KafkaDataIngestionSourceService(IDataStoreService<TModel> dataStoreService, KafkaDataIngestionSourceOptions options)
    {
        _dataStoreService = dataStoreService;
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = options.BootstrapServers,
            GroupId = options.GroupId
        };
        _topic = options.Topic;
        _consumer = new ConsumerBuilder<Guid, TModel>(consumerConfig).Build();
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
            var model = consumeResult.Message.Value;
            model.Id = consumeResult.Message.Key;
            _ = _dataStoreService.CreateSingleAsync(model, cancellationToken);
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
