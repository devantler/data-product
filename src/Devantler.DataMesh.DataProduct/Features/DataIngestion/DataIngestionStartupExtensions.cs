using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;

namespace Devantler.DataMesh.DataProduct.Features.DataIngestion;

/// <summary>
/// Extensions for registering data ingestion sources and configuring the web application to use them.
/// </summary>
public static partial class DataIngestionStartupExtensions
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

        services.AddGeneratedServiceRegistrations(options.Services.DataIngestionSources);
        _ = services.AddHostedService<LocalDataIngestionSourceService<Student>>();

        return services;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, List<IDataIngestionSourceOptions> dataIngestionSources);
}
