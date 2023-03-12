using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.DataIngestion.Services;
using Devantler.DataMesh.DataProduct.Schemas;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion;

/// <summary>
/// Extensions for registering data ingestion sources and configuring the web application to use them.
/// </summary>
public static class DataIngestionStartupExtensions
{
    /// <summary>
    /// Registers data ingestion sources to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddDataIngestion(this IServiceCollection services, DataProductOptions options)
    {
        if (!options.Services.DataIngestionSources.Any())
            return services;

        //TODO: Move this code to a DataIngestionStartupExtensionsGenerator class.
        // foreach (var dataIngestionSource in options.Services.DataIngestionSources)
        // {
        // _ = dataIngestionSource.Type switch
        // {
        _ = services.AddHostedService<LocalDataIngestionSourceService<Student>>();
        // _ => throw new NotSupportedException($"Data ingestion source '{dataIngestionSource}' is not supported."),
        // };
        // }

        return services;
    }
}
