using Chr.Avro.Confluent;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;

/// <summary>
/// A data ingestion source service that ingests data from a Kafka topic.
/// </summary>
public class KafkaDataIngestorService<TKey, TSchema> : BackgroundService
    where TSchema : class, Schemas.ISchema<TKey>
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;
    readonly List<(IConsumer<TKey, TSchema>, string)> _consumers;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaDataIngestorService{TKey, TSchema}"/> class.
    /// </summary>
    public KafkaDataIngestorService(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TKey, TSchema>>();
        var dataProductOptions = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
        var dataIngestorOptions = dataProductOptions.DataIngestors
            .Where(x => x.Type == DataIngestorType.Kafka)
            .Cast<KafkaDataIngestorOptions>();

        var registryConfig = new SchemaRegistryConfig
        {
            Url = dataProductOptions.SchemaRegistry.Url
        };

        var registry = new CachedSchemaRegistryClient(registryConfig);
        _consumers = new List<(IConsumer<TKey, TSchema>, string)>();
        foreach (var options in dataIngestorOptions)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = options.Servers,
                GroupId = options.GroupId
            };
            var consumer = new ConsumerBuilder<TKey, TSchema>(consumerConfig)
                .SetAvroKeyDeserializer(registry)
                .SetAvroValueDeserializer(registry)
                .SetErrorHandler((_, error) => Console.Error.WriteLine(error.ToString()))
                .Build();
            _consumers.Add(new(consumer, options.Topic));
        }
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Parallel.ForEachAsync(_consumers, stoppingToken, async (item, token) =>
        {
            var consumer = item.Item1;
            string topic = item.Item2;
            consumer.Subscribe(topic);
            while (!token.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(token);
                var schema = consumeResult.Message.Value;

                // TODO: set ids to default value.

                _ = await _dataStoreService.CreateSingleAsync(schema, token);
            }
        });
    }
}