using Chr.Avro.Confluent;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Inputs;
using Devantler.DataProduct.Features.DataStore.Services;
using Devantler.DataProduct.Features.Schemas;
using Microsoft.Extensions.Options;

namespace Devantler.DataProduct.Features.Inputs.Services;

/// <summary>
/// A  service that ingests data from a Kafka topic.
/// </summary>
public class KafkaInputService<TKey, TSchema> : BackgroundService where TSchema : class, ISchema<TKey>
{
  readonly IDataStoreService<TKey, TSchema> _dataStoreService;
  readonly List<(IConsumer<string, TSchema>, string)> _consumers;

  /// <summary>
  /// Initializes a new instance of the <see cref="KafkaInputService{TKey, TSchema}"/> class.
  /// </summary>
  public KafkaInputService(IServiceScopeFactory scopeFactory)
  {
    var scope = scopeFactory.CreateScope();
    _dataStoreService = scope.ServiceProvider.GetRequiredService<IDataStoreService<TKey, TSchema>>();
    var dataProductOptions = scope.ServiceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
    var dataIngestorOptions = dataProductOptions.Inputs
        .Where(x => x.Type == InputType.Kafka)
        .Cast<KafkaInputOptions>();

    var registryConfig = new SchemaRegistryConfig
    {
      Url = dataProductOptions.SchemaRegistry.Url
    };

    var registry = new CachedSchemaRegistryClient(registryConfig);
    _consumers = [];
    foreach (var options in dataIngestorOptions)
    {
      var consumerConfig = new ConsumerConfig
      {
        BootstrapServers = options.Servers,
        GroupId = options.GroupId
      };
      var consumer = new ConsumerBuilder<string, TSchema>(consumerConfig)
          .SetAvroValueDeserializer(registry)
          .SetErrorHandler((_, error) => Console.Error.WriteLine(error.ToString()))
          .Build();
      _consumers.Add(new ValueTuple<IConsumer<string, TSchema>, string>(consumer, options.Topic));
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

        if (schema is null)
          continue;

        _ = await _dataStoreService.CreateSingleAsync(schema, token);
      }
    });
  }

  /// <inheritdoc />
  public override async Task StopAsync(CancellationToken cancellationToken = default)
  {
    foreach (var consumer in _consumers)
    {
      consumer.Item1.Close();
      consumer.Item1.Dispose();
    }
    await base.StopAsync(cancellationToken);
  }
}
