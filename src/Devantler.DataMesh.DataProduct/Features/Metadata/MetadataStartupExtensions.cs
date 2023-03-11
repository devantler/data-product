using Devantler.DataMesh.DataProduct.Configuration.Options;

namespace Devantler.DataMesh.DataProduct.Features.Metadata;

/// <summary>
/// Extensions to register metadata integration to the DI container and configure the web application to use it.
/// </summary>
public static class MetadataStartupExtensions
{
    /// <summary>
    /// Registers metadata collection and integrations to the DI container.
    /// </summary>
    public static IServiceCollection AddMetadata(this IServiceCollection services, DataProductOptions options)
        => services;

    /// <summary>
    /// Configures the web application to use metadata collection and integrations.
    /// </summary>
    public static IApplicationBuilder UseMetadata(this IApplicationBuilder app, DataProductOptions options)
        => app;
}