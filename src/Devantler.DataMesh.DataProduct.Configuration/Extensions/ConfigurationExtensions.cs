using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;
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
        var dataProductOptions = configuration.GetSection(DataProductOptions.Key).Get<DataProductOptions>()
                                 ?? throw new InvalidOperationException(
                                     $"Failed to bind configuration section '{DataProductOptions.Key}' to the type '{typeof(DataProductOptions).FullName}'."
                                 );

        dataProductOptions.Services.SchemaRegistry = dataProductOptions.Services.SchemaRegistry.Type switch
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
            _ => throw new NotSupportedException($"Schema registry type '{dataProductOptions.Services.SchemaRegistry.Type}' is not supported.")
        };

        var dataIngestors = new List<IDataIngestorOptions>();

        if (dataProductOptions.FeatureFlags.EnableDataIngestion)
        {
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
        }

        dataProductOptions.Services.DataIngestors = dataIngestors;

        return dataProductOptions;
    }
}