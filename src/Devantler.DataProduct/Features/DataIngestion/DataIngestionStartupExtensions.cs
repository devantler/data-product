#pragma warning disable S3251
using Devantler.DataProduct.Core.Configuration.Options;
using Devantler.DataProduct.Core.Configuration.Options.DataIngestors;

namespace Devantler.DataProduct.Features.DataIngestion;

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
        if (!options.DataIngestors.Any())
            return services;

        services.AddGeneratedServiceRegistrations(options.DataIngestors);

        return services;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services,
        List<DataIngestorOptions> options);
}
#pragma warning restore S3251