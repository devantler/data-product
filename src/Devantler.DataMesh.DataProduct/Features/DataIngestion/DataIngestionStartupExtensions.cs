using Devantler.DataMesh.DataProduct.Configuration.Options;

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
        => services;
    // {
    //     return (options.FeatureFlags.EnableDataIngestionSources || !options.Services.DataIngestionSources.Any())
    //         ? services.AddHostedService<KafkaDataIngestionSourceService<Student>>()
    //         : services;
    // }

    /// <summary>
    /// Configures the web application to use enabled data ingestion sources.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static IApplicationBuilder UseDataIngestion(this IApplicationBuilder app, DataProductOptions options)
        => app;
    // {
    //     if (!options.FeatureFlags.EnableDataIngestionSources || !options.Services.DataIngestionSources.Any())
    //         return app;

    //     var contosoUniversityDataIngestionSourceService = app.Services.GetRequiredService<KafkaDataIngestionSourceService<Student>>();
    //     _ = contosoUniversityDataIngestionSourceService.StartAsync(CancellationToken.None);
    //     return app;
    // }
}
