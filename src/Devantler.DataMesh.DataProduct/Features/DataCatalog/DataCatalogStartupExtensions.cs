using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataCatalog;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog;

/// <summary>
/// Extensions to register a data catalog integration to the DI container and configure the web application to use it.
/// </summary>
public static class DataCatalogStartupExtensions
{
    /// <summary>
    /// Registers a data catalog integration to the DI container.
    /// </summary>
    public static IServiceCollection AddDataCatalog(this IServiceCollection services, DataProductOptions options)
    {
        _ = options.DataCatalog?.Type switch
        {
            DataCatalogType.DataHub => services.AddHttpClient<IDataCatalogService, DataHubDataCatalogService>(),
            _ => throw new NotSupportedException($"The data catalog type '{options.DataCatalog?.Type}' is not supported.")
        };
        return services;
    }

    /// <summary>
    /// Configures the web application to use a data catalog integration.
    /// </summary>
    public static IApplicationBuilder UseDataCatalog(this IApplicationBuilder app)
        => app;
}