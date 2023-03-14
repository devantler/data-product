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
public class KafkaDataIngestor<TSchema> : BackgroundService, IDataIngestor
    where TSchema : class, Schemas.ISchema
{
    readonly IDataStoreService<TSchema> _dataStoreService;
    readonly List<KeyValuePair<IConsumer<string, TSchema>, string>> _consumers;

    /// <summary>
    /// Initializes a new instance of the <see cref="KafkaDataIngestor{TSchema}"/> class.
    /// </summary>
    public KafkaDataIngestor(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TSchema>>();
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
        _consumers = new List<KeyValuePair<IConsumer<string, TSchema>, string>>();
        foreach (var options in dataIngestorOptions)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = options.BootstrapServers,
                GroupId = options.GroupId
            };
            var consumer = new ConsumerBuilder<string, TSchema>(consumerConfig)
                .SetAvroKeyDeserializer(registry)
                .SetAvroValueDeserializer(registry)
                .SetErrorHandler((_, error) => Console.Error.WriteLine(error.ToString()))
                .Build();
            _consumers.Add(new KeyValuePair<IConsumer<string, TSchema>, string>(consumer, options.Topic));
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

                SetIdsRecursively(schema, consumeResult.Message.Key);

                _ = await _dataStoreService.CreateSingleAsync(schema, token);
            }
        });
    }

    /// <summary>
    /// Sets all Id properties in the given object.
    /// </summary>
    /// <remarks>
    /// The Id property on the root object is set to the give key. 
    /// The rest of the Id properties are set to an empty string, so the datastore can generate a new Id without conflict.
    /// </remarks>
    /// <param name="obj">The object to set the Id properties on.</param>
    /// <param name="key">The key to set the Id property to.</param>
    public void SetIdsRecursively(object obj, string key)
    {
        if (obj == null) return;

        var type = obj.GetType();

        // If the type is a dictionary, set the Id value for each key-value pair
        if (typeof(IDictionary).IsAssignableFrom(type))
        {
            foreach (object? dictKey in ((IDictionary)obj).Keys)
            {
                if (dictKey.ToString() == "Id")
                {
                    if (((IDictionary)obj)[dictKey] == null)
                    {
                        ((IDictionary)obj)[dictKey] = string.Empty;
                    }
                }
                else
                {
                    SetIdsRecursively(((IDictionary)obj)[dictKey] ?? null!, string.Empty);
                }
            }
        }
        // If the type is an array, set the Id value for each element
        else if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            foreach (object? item in (IEnumerable)obj)
            {
                SetIdsRecursively(item, string.Empty);
            }
        }
        // Otherwise, set the Id value for each property
        else
        {
            foreach (var prop in type.GetProperties())
            {
                if (prop.Name == "Id" && prop.GetValue(obj) == null)
                {
                    prop.SetValue(obj, key);
                }
                else
                {
                    SetIdsRecursively(prop.GetValue(obj) ?? null!, string.Empty);
                }
            }
        }
    }
}