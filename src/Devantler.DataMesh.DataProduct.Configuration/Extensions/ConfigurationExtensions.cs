using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Configuration.Extensions;

/// <summary>
/// Extension methods for <see cref="IConfiguration"/>.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Gets the data product options.
    /// </summary>
    public static DataProductOptions GetDataProductOptions(this IConfiguration configuration)
    {
        var dataProductOptions = configuration.Get<DataProductOptions>()
            ?? throw new InvalidOperationException(
                $"Failed to bind configuration to the type '{typeof(DataProductOptions).FullName}'."
            );

        ConfigureSchemaRegistryOptions(configuration, dataProductOptions);
        ConfigureCacheStoreOptions(configuration, dataProductOptions);
        ConfigureDataIngestorsOptions(configuration, dataProductOptions);

        return dataProductOptions;
    }

    private static void ConfigureSchemaRegistryOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        dataProductOptions.SchemaRegistry = dataProductOptions.SchemaRegistry.Type switch
        {
            SchemaRegistryType.Kafka => configuration.GetSection(ISchemaRegistryOptions.Key)
                .Get<KafkaSchemaRegistryOptions>()
                    ?? throw new InvalidOperationException(
                        $"Failed to bind configuration section '{ISchemaRegistryOptions.Key}' to the type '{typeof(KafkaSchemaRegistryOptions).FullName}'."
                    ),
            SchemaRegistryType.Local => configuration.GetSection(ISchemaRegistryOptions.Key)
                .Get<LocalSchemaRegistryOptions>()
                    ?? throw new InvalidOperationException(
                        $"Failed to bind configuration section '{ISchemaRegistryOptions.Key}' to the type '{typeof(LocalSchemaRegistryOptions).FullName}'."
                    ),
            _ => throw new NotSupportedException($"Schema registry type '{dataProductOptions.SchemaRegistry.Type}' is not supported.")
        };
    }

    private static void ConfigureCacheStoreOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (dataProductOptions.FeatureFlags.EnableCaching)
        {
            dataProductOptions.CacheStore = dataProductOptions.CacheStore.Type switch
            {
                CacheStoreType.InMemory => configuration.GetSection(ICacheStoreOptions.Key)
                    .Get<InMemoryCacheStoreOptions>()
                        ?? throw new InvalidOperationException(
                            $"Failed to bind configuration section '{ICacheStoreOptions.Key}' to the type '{typeof(InMemoryCacheStoreOptions).FullName}'."
                        ),
                CacheStoreType.Redis => throw new NotImplementedException($"Cache store type '{dataProductOptions.CacheStore.Type}' is not implemented yet."),
                _ => throw new NotSupportedException($"Cache store type '{dataProductOptions.CacheStore.Type}' is not supported.")
            };
        }
    }

    private static void ConfigureDataIngestorsOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (dataProductOptions.FeatureFlags.EnableDataIngestion)
        {
            var dataIngestors = new List<IDataIngestorOptions>();
            var localDataIngestorOptions = configuration.GetSection(IDataIngestorOptions.Key)
                .Get<List<LocalDataIngestorOptions>>()
                .Where(x => x.Type == DataIngestorType.Local);

            dataIngestors.AddRange(
                localDataIngestorOptions
            );

            dataIngestors.AddRange(
                configuration.GetSection(IDataIngestorOptions.Key)
                    .Get<List<KafkaDataIngestorOptions>>()
                    .Where(x => x.Type == DataIngestorType.Kafka)
            );
            dataProductOptions.DataIngestors = dataIngestors;
        }
    }
}