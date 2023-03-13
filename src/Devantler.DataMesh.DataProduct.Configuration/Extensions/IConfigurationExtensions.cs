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

        var dataIngestors = new List<IDataIngestorOptions>();

        dataIngestors.AddRange(
            configuration.GetSection(IDataIngestorOptions.Key)
                .Get<List<LocalDataIngestorOptions>>()
                .Where(x => x.Type == DataIngestorType.Local)
        );

        dataIngestors.AddRange(
            configuration.GetSection(IDataIngestorOptions.Key)
                .Get<List<KafkaDataIngestorOptions>>()
                .Where(x => x.Type == DataIngestorType.Kafka)
        );

        dataProductOptions.Services.DataIngestors = dataIngestors;

        return dataProductOptions;
    }
}