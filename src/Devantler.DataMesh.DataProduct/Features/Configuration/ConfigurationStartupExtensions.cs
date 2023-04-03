using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.Configuration.Extensions;

namespace Devantler.DataMesh.DataProduct.Features.Configuration;

/// <summary>
/// Extensions for registering configuration to the DI container.
/// </summary>
public static class ConfigurationStartupExtensions
{
    /// <summary>
    /// Adds and configures a data product configuration from command line arguments, environment variables, config.{environment}.{json|yml|yaml}, and config.{json|yml|yaml}.
    /// </summary>
    public static DataProductOptions AddConfiguration(this WebApplicationBuilder builder, string[] args)
    {
        _ = builder.Configuration.AddDataProductConfiguration(builder.Environment, args);
        var options = builder.Configuration.GetDataProductOptions();

        _ = builder.Services.AddOptions<DataProductOptions>().Configure(o =>
        {
            o.Name = options.Name;
            o.Description = options.Description;
            o.Release = options.Release;
            o.PublicUrl = options.PublicUrl;
            o.License = options.License;
            o.Owner = options.Owner;
            o.FeatureFlags = options.FeatureFlags;
            o.Apis = options.Apis;
            o.CacheStore = options.CacheStore;
            o.Dashboard = options.Dashboard;
            o.DataCatalog = options.DataCatalog;
            o.DataIngestors = options.DataIngestors;
            o.DataStore = options.DataStore;
            o.LoggingExporter = options.LoggingExporter;
            o.MetricsExporter = options.MetricsExporter;
            o.SchemaRegistry = options.SchemaRegistry;
            o.TracingExporter = options.TracingExporter;
        });

        return options;
    }
}