using System.Net.Http.Headers;
using Devantler.DataProduct.Core.Configuration.Options;
using Devantler.DataProduct.Core.Configuration.Options.DataCatalog;
using Devantler.DataProduct.Features.DataCatalog.Services;

namespace Devantler.DataProduct.Features.DataCatalog;

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
        switch (options.DataCatalog.Type)
        {
            case DataCatalogType.DataHub:
                _ = services.AddHttpClient<Services.DataHubClient.Client>(client =>
                {
                    client.BaseAddress = new Uri(options.DataCatalog.Url);
                    if (!string.IsNullOrEmpty(options.DataCatalog.AccessToken))
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.DataCatalog.AccessToken);
                    }
                });
                _ = services.AddHostedService<DataHubDataCatalogService>();
                break;
            default:
                throw new NotSupportedException($"The data catalog type '{options.DataCatalog.Type}' is not supported.");
        }
        return services;
    }

    /// <summary>
    /// Configures the web application to use a data catalog integration.
    /// </summary>
    public static IApplicationBuilder UseDataCatalog(this IApplicationBuilder app)
        => app;
}