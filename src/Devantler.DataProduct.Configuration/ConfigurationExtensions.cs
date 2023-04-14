using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.CacheStore;
using Devantler.DataProduct.Configuration.Options.DataCatalog;
using Devantler.DataProduct.Configuration.Options.DataIngestors;
using Devantler.DataProduct.Configuration.Options.DataStore;
using Devantler.DataProduct.Configuration.Options.DataStore.SQL;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry.Providers;
using Devantler.DataProduct.Configuration.Options.Telemetry;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataProduct.Configuration;

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
        var dataProductOptions = configuration.Get<DataProductOptions>() ?? throw new InvalidOperationException($"Failed to bind configuration to the type '{typeof(DataProductOptions).FullName}'.");

        ConfigureDataStoreOptions(configuration, dataProductOptions);
        ConfigureCacheStoreOptions(configuration, dataProductOptions);
        ConfigureDataCatalogOptions(configuration, dataProductOptions);
        ConfigureDataIngestorsOptions(configuration, dataProductOptions);
        ConfigureTelemetryExporterOptions(configuration, dataProductOptions);
        ConfigureSchemaRegistryOptions(configuration, dataProductOptions);

        return dataProductOptions;
    }

    static void ConfigureDataStoreOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        dataProductOptions.DataStore = (dataProductOptions.DataStore.Type, dataProductOptions.DataStore.Provider) switch
        {
            (DataStoreType.SQL, SQLDataStoreProvider.Sqlite) => dataProductOptions.DataStore,
            (DataStoreType.SQL, SQLDataStoreProvider.PostgreSQL) => configuration.GetSection(DataStoreOptions.Key).Get<PostgreSQLDataStoreOptions>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{DataStoreOptions.Key}' to the type '{typeof(PostgreSQLDataStoreOptions).FullName}'."),
            _ => throw new NotSupportedException($"Data store type '{dataProductOptions.DataStore.Type}' and provider '{dataProductOptions.DataStore.Provider}' is not supported.")
        };
    }

    static void ConfigureCacheStoreOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (dataProductOptions.FeatureFlags.EnableCaching)
        {
            dataProductOptions.CacheStore = dataProductOptions.CacheStore.Type switch
            {
                CacheStoreType.InMemory => dataProductOptions.CacheStore,
                CacheStoreType.Redis => configuration.GetSection(CacheStoreOptions.Key).Get<RedisCacheStoreOptions>()
                    ?? throw new InvalidOperationException($"Failed to bind configuration section '{CacheStoreOptions.Key}' to the type '{typeof(RedisCacheStoreOptions).FullName}'."),
                _ => throw new NotSupportedException($"Cache store type '{dataProductOptions.CacheStore.Type}' is not supported.")
            };
        }
    }

    static void ConfigureDataCatalogOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (!dataProductOptions.FeatureFlags.EnableDataCatalog) return;

        if (dataProductOptions.DataCatalog == null)
        {
            throw new InvalidOperationException($"The configuration section '{DataCatalogOptions.Key}' is invalid or missing.");
        }

        dataProductOptions.DataCatalog = dataProductOptions.DataCatalog.Type switch
        {
            DataCatalogType.DataHub => configuration.GetSection(DataCatalogOptions.Key).Get<DataHubDataCatalogOptions>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{DataCatalogOptions.Key}' to the type '{typeof(DataHubDataCatalogOptions).FullName}'."),
            _ => throw new NotSupportedException($"Data catalog type '{dataProductOptions.DataCatalog.Type}' is not supported.")
        };
    }

    static void ConfigureDataIngestorsOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (!dataProductOptions.FeatureFlags.EnableDataIngestion) return;

        var dataIngestors = new List<DataIngestorOptions>();
        var localDataIngestorOptions
            = (configuration.GetSection(DataIngestorOptions.Key).Get<List<LocalDataIngestorOptions>>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{DataIngestorOptions.Key}' to the type '{typeof(LocalDataIngestorOptions).FullName}'.")).Where(x => x.Type == DataIngestorType.Local);

        dataIngestors.AddRange(localDataIngestorOptions);

        dataIngestors.AddRange((configuration.GetSection(DataIngestorOptions.Key).Get<List<KafkaDataIngestorOptions>>()
            ?? throw new InvalidOperationException($"Failed to bind configuration section '{DataIngestorOptions.Key}' to the type '{typeof(KafkaDataIngestorOptions).FullName}'.")).Where(x => x.Type == DataIngestorType.Kafka));
        dataProductOptions.DataIngestors = dataIngestors;
    }

    static void ConfigureTelemetryExporterOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (dataProductOptions.FeatureFlags.EnableTelemetry)
        {
            dataProductOptions.TelemetryExporter = dataProductOptions.TelemetryExporter.Type switch
            {
                TelemetryExporterType.OpenTelemetry => configuration.GetSection(TelemetryOptions.Key).Get<OpenTelemetryOptions>()
                    ?? throw new InvalidOperationException($"Failed to bind configuration section '{TelemetryOptions.Key}' to the type '{typeof(OpenTelemetryOptions).FullName}'."),
                TelemetryExporterType.Console => configuration.GetSection(TelemetryOptions.Key).Get<ConsoleTelemetryOptions>()
                    ?? throw new InvalidOperationException($"Failed to bind configuration section '{TelemetryOptions.Key}' to the type '{typeof(ConsoleTelemetryOptions).FullName}'."),
                _ => throw new NotSupportedException($"Logging system type '{dataProductOptions.TelemetryExporter.Type}' is not supported.")
            };
        }
    }

    static void ConfigureSchemaRegistryOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        dataProductOptions.SchemaRegistry = dataProductOptions.SchemaRegistry.Type switch
        {
            SchemaRegistryType.Kafka => configuration.GetSection(SchemaRegistryOptions.Key).Get<KafkaSchemaRegistryOptions>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{SchemaRegistryOptions.Key}' to the type '{typeof(KafkaSchemaRegistryOptions).FullName}'."),
            SchemaRegistryType.Local => configuration.GetSection(SchemaRegistryOptions.Key).Get<LocalSchemaRegistryOptions>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{SchemaRegistryOptions.Key}' to the type '{typeof(LocalSchemaRegistryOptions).FullName}'."),
            _ => throw new NotSupportedException($"Schema registry type '{dataProductOptions.SchemaRegistry.Type}' is not supported.")
        };
    }
}