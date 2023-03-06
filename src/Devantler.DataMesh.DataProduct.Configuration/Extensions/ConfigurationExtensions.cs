using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.DocumentBased;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions.Providers;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Configuration.Extensions;

/// <summary>
/// Extensions for the <see cref="IConfiguration"/> interface to get the data product options.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Gets the data product options from the configuration.
    /// </summary>
    /// <param name="configuration"></param>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static DataProductOptions GetDataProductOptions(this IConfiguration configuration)
    {
        var dataProductOptions = configuration.GetSection(DataProductOptions.Key).Get<DataProductOptions>()
            ?? throw new InvalidOperationException($"The configuration section '{DataProductOptions.Key}' is missing.");

        if (configuration.GetSection(DataStoreOptionsBase.Key).Exists())
            dataProductOptions.Services.DataStore = SetDataStoreOptions(configuration);

        if (configuration.GetSection(SchemaRegistryOptionsBase.Key).Exists())
            dataProductOptions.Services.SchemaRegistry = SetSchemaRegistryOptions(configuration);

        return dataProductOptions;
    }

    /// <summary>
    /// Gets the data store options from the configuration.
    /// </summary>
    /// <param name="configuration"></param>
    public static IDataStoreOptions SetDataStoreOptions(IConfiguration configuration)
    {
        var dataStoreType = configuration.GetSection(DataStoreOptionsBase.Key).GetValue<DataStoreType>("Type");
        string dataStoreProvider = configuration.GetSection(DataStoreOptionsBase.Key).GetValue<string>("Provider") ?? throw new InvalidOperationException($"The configuration section '{DataStoreOptionsBase.Key}' is missing the property 'Provider'.");
        return (dataStoreType, dataStoreProvider) switch
        {
            (DataStoreType.Relational, nameof(RelationalDataStoreProvider.Sqlite)) => configuration.GetSection(DataStoreOptionsBase.Key).Get<SqliteDataStoreOptions>()
                ?? throw new InvalidOperationException($"Failed to bind the configuration instance '{nameof(SqliteDataStoreOptions)}' to the configuration section '{DataStoreOptionsBase.Key}"),
            (DataStoreType.DocumentBased, nameof(DocumentBasedDataStoreProvider.MongoDb)) => configuration.GetSection(DataStoreOptionsBase.Key).Get<MongoDbDataStoreOptions>()
                ?? throw new InvalidOperationException($"Failed to bind the configuration instance '{nameof(MongoDbDataStoreOptions)}' to the configuration section '{DataStoreOptionsBase.Key}"),
            _ => throw new NotImplementedException($"The combination of the data store type '{dataStoreType}' and the data store provider '{dataStoreProvider}' is not implemented yet.")
        };
    }

    /// <summary>
    /// Gets the schema registry options from the configuration.
    /// </summary>
    /// <param name="configuration"></param>
    public static ISchemaRegistryOptions SetSchemaRegistryOptions(IConfiguration configuration)
    {
        var schemaRegistryType = configuration.GetSection(SchemaRegistryOptionsBase.Key).GetValue<SchemaRegistryType>("Type");

        return schemaRegistryType switch
        {
            SchemaRegistryType.Local => configuration.GetSection(SchemaRegistryOptionsBase.Key).Get<LocalSchemaRegistryOptions>()
                ?? throw new InvalidOperationException($"Failed to bind the configuration instance '{nameof(LocalSchemaRegistryOptions)}' to the configuration section '{SchemaRegistryOptionsBase.Key}"),
            SchemaRegistryType.Kafka => configuration.GetSection(SchemaRegistryOptionsBase.Key).Get<KafkaSchemaRegistryOptions>()
                ?? throw new InvalidOperationException($"Failed to bind the configuration instance '{nameof(KafkaSchemaRegistryOptions)}' to the configuration section '{SchemaRegistryOptionsBase.Key}"),
            _ => throw new NotImplementedException($"The schema registry type '{schemaRegistryType}' is not implemented yet.")
        };
    }
}
