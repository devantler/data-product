namespace Devantler.DataMesh.DataProduct.Features.DataIngestionSources;

/// <summary>
/// Extensions for registering data ingestion sources and configuring the web application to use them.
/// </summary>
public static class DataIngestionSourceStartupExtensions
{
    // /// <summary>
    // /// Registers data ingestion sources to the DI container.
    // /// </summary>
    // /// <param name="services"></param>
    // /// <param name="options"></param>
    // public static IServiceCollection AddDataIngestionSources(this IServiceCollection services, DataProductOptions options)
    // {
    //     return (options.FeatureFlags.EnableDataIngestionSources || !options.Services.DataIngestionSources.Any())
    //         ? services.AddHostedService<KafkaDataIngestionSourceService<Student>>()
    //         : services;
    // }

    // /// <summary>
    // /// Configures the web application to use enabled data ingestion sources.
    // /// </summary>
    // /// <param name="app"></param>
    // /// <param name="options"></param>
    // public static WebApplication UseDataIngestionSources(this WebApplication app, DataProductOptions options)
    // {
    //     if (!options.FeatureFlags.EnableDataIngestionSources || !options.Services.DataIngestionSources.Any())
    //         return app;

    //     var contosoUniversityDataIngestionSourceService = app.Services.GetRequiredService<KafkaDataIngestionSourceService<Student>>();
    //     _ = contosoUniversityDataIngestionSourceService.StartAsync(CancellationToken.None);
    //     return app;
    // }
}
