using System.Collections;
using Chr.Avro.Confluent;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion.Ingestors;

/// <summary>
/// A data ingestion source service that ingests data from a Kafka topic.
/// </summary>
public class KafkaDataIngestor<TKey, TSchema> : BackgroundService, IDataIngestor
    where TSchema : class, Schemas.ISchema<TKey>
{
    readonly IDataStoreService<TKey, TSchema> _dataStoreService;
    readonly List<KeyValuePair<IConsumer<TKey, TSchema>, string>> _consumers;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaDataIngestor{TKey, TSchema}"/> class.
    /// </summary>
    public KafkaDataIngestor(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TKey, TSchema>>();
        var dataProductOptions = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
        var dataIngestorOptions = dataProductOptions.Services.DataIngestors
            .Where(x => x.Type == DataIngestorType.Kafka)
            .Cast<KafkaDataIngestorOptions>();
        var schemaRegistryOptions = dataProductOptions.Services.SchemaRegistry as KafkaSchemaRegistryOptions
            ?? throw new InvalidCastException("Unable to cast schema registry to kafka schema registry.");

        var registryConfig = new SchemaRegistryConfig
        {
            Url = schemaRegistryOptions.Url
        };

        var registry = new CachedSchemaRegistryClient(registryConfig);
        _consumers = new List<KeyValuePair<IConsumer<TKey, TSchema>, string>>();
        foreach (var options in dataIngestorOptions)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = options.BootstrapServers,
                GroupId = options.GroupId
            };
            var consumer = new ConsumerBuilder<TKey, TSchema>(consumerConfig)
                .SetAvroKeyDeserializer(registry)
                .SetAvroValueDeserializer(registry)
                .SetErrorHandler((_, error) => Console.Error.WriteLine(error.ToString()))
                .Build();
            _consumers.Add(new KeyValuePair<IConsumer<TKey, TSchema>, string>(consumer, options.Topic));
        }
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Parallel.ForEachAsync(_consumers, stoppingToken, async (item, token) =>
        {
            var consumer = item.Key;
            string topic = item.Value;
            consumer.Subscribe(topic);
            while (!token.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(token);
                var schema = consumeResult.Message.Value;

                schema.Id = consumeResult.Message.Key;

                _ = await _dataStoreService.CreateSingleAsync(schema, token);
            }
        });
    }
}