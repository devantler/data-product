using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Auth;
using Devantler.DataProduct.Configuration.Options.CacheStore;
using Devantler.DataProduct.Configuration.Options.DataCatalog;
using Devantler.DataProduct.Configuration.Options.DataIngestors;
using Devantler.DataProduct.Configuration.Options.DataStore;
using Devantler.DataProduct.Configuration.Options.DataStore.SQL;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry.Providers;
using Devantler.DataProduct.Configuration.Options.Telemetry;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataProduct.Configuration.Extensions;

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

        ConfigureAuthenticationOptions(configuration, dataProductOptions);
        ConfigureDataStoreOptions(configuration, dataProductOptions);
        ConfigureCacheStoreOptions(configuration, dataProductOptions);
        ConfigureDataCatalogOptions(configuration, dataProductOptions);
        ConfigureDataIngestorsOptions(configuration, dataProductOptions);
        ConfigureTelemetryExporterOptions(configuration, dataProductOptions);
        ConfigureSchemaRegistryOptions(configuration, dataProductOptions);

        return dataProductOptions;
    }

    static void ConfigureAuthenticationOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (!dataProductOptions.FeatureFlags.EnableAuth) return;

        if (dataProductOptions.Auth is null)
            throw new InvalidOperationException($"Authentication is enabled but no options are configured. This is often a result of the configuration section '{DataCatalogOptions.Key}' being invalid or missing.");

        dataProductOptions.Auth = dataProductOptions.Auth.Type switch
        {
            AuthType.Keycloak => configuration.GetSection(AuthOptions.Key).Get<KeycloakAuthOptions>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{AuthOptions.Key}' to the type '{typeof(KeycloakAuthOptions).FullName}'."),
            _ => throw new NotSupportedException($"Authentication type '{dataProductOptions.Auth.Type}' is not supported.")
        };
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
            throw new InvalidOperationException($"DataCatalog is enabled but no options are configured. This is often a result of the configuration section '{DataCatalogOptions.Key}' being invalid or missing.");

        dataProductOptions.DataCatalog = dataProductOptions.DataCatalog.Type switch
        {
            DataCatalogType.DataHub => configuration.GetSection(DataCatalogOptions.Key).Get<DataHubDataCatalogOptions>()
                ?? throw new InvalidOperationException($"Failed to bind configuration section '{DataCatalogOptions.Key}' to the type '{typeof(DataHubDataCatalogOptions).FullName}'."),
            _ => throw new NotSupportedException($"Data catalog type '{dataProductOptions.DataCatalog.Type}' is not supported.")
        };
    }

    static void ConfigureDataIngestorsOptions(IConfiguration configuration, DataProductOptions dataProductOptions)
    {
        if (!dataProductOptions.FeatureFlags.EnableDataIngestion || !dataProductOptions.DataIngestors.Any()) return;

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
            dataProductOptions.Telemetry = dataProductOptions.Telemetry.ExporterType switch
            {
                TelemetryExporterType.OpenTelemetry => configuration.GetSection(TelemetryOptions.Key).Get<OpenTelemetryOptions>()
                    ?? throw new InvalidOperationException($"Failed to bind configuration section '{TelemetryOptions.Key}' to the type '{typeof(OpenTelemetryOptions).FullName}'."),
                TelemetryExporterType.Console => configuration.GetSection(TelemetryOptions.Key).Get<ConsoleTelemetryOptions>()
                    ?? dataProductOptions.Telemetry,
                _ => throw new NotSupportedException($"Logging system type '{dataProductOptions.Telemetry.ExporterType}' is not supported.")
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
                ?? dataProductOptions.SchemaRegistry,
            _ => throw new NotSupportedException($"Schema registry type '{dataProductOptions.SchemaRegistry.Type}' is not supported.")
        };
    }
}