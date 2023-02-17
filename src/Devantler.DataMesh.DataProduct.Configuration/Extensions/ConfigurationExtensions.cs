using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions.Providers;
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

        dataProductOptions.DataStoreOptions ??= GetDataStoreOptions(configuration);

        dataProductOptions.SchemaRegistryOptions ??= GetSchemaRegistryOptions(configuration);

        return dataProductOptions;
    }

    /// <summary>
    /// Gets the data store options from the configuration.
    /// </summary>
    /// <param name="configuration"></param>
    public static IDataStoreOptions GetDataStoreOptions(IConfiguration configuration)
    {
        var dataStoreType = configuration.GetSection(DataStoreOptionsBase.Key).GetValue<DataStoreType>("Type");
        var dataStoreProvider = dataStoreType switch
        {
            DataStoreType.Relational => configuration.GetSection(DataStoreOptionsBase.Key).GetValue<RelationalDataStoreProvider>("Provider"),
            _ => throw new NotImplementedException($"The data store type '{dataStoreType}' is not implemented yet.")
        };

        return (dataStoreType, dataStoreProvider) switch
        {
            (DataStoreType.Relational, RelationalDataStoreProvider.SQLite) => configuration.GetSection(DataStoreOptionsBase.Key).Get<SqliteDataStoreOptions>()
                ?? throw new InvalidOperationException($"Failed to bind the configuration instance '{nameof(SqliteDataStoreOptions)}' to the configuration section '{DataStoreOptionsBase.Key}"),
            _ => throw new NotImplementedException($"The combination of the data store type '{dataStoreType}' and the data store provider '{dataStoreProvider}' is not implemented yet.")
        };
    }

    /// <summary>
    /// Gets the schema registry options from the configuration.
    /// </summary>
    /// <param name="configuration"></param>
    public static ISchemaRegistryOptions GetSchemaRegistryOptions(IConfiguration configuration)
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
