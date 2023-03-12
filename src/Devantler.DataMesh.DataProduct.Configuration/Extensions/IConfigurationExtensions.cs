using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Configuration.Extensions;

/// <summary>
/// IConfiguration extensions.
/// </summary>
public static class IConfigurationExtensions
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

        var dataIngestionSources = new List<IDataIngestionSourceOptions>();

        dataIngestionSources.AddRange(
            configuration.GetSection(IDataIngestionSourceOptions.Key)
                .Get<List<LocalDataIngestionSourceOptions>>()
                .Where(x => x.Type == DataIngestionSourceType.Local)
        );

        dataIngestionSources.AddRange(
            configuration.GetSection(IDataIngestionSourceOptions.Key)
                .Get<List<KafkaDataIngestionSourceOptions>>()
                .Where(x => x.Type == DataIngestionSourceType.Kafka)
        );

        dataProductOptions.Services.DataIngestionSources = dataIngestionSources;

        return dataProductOptions;
    }
}