using System.Reflection;
using Devantler.DataMesh.DataProduct.Configuration.Extensions;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataMesh.DataProduct.Features.Apis;
using Devantler.DataMesh.DataProduct.Features.Caching;
using Devantler.DataMesh.DataProduct.Features.DataEgestion;
using Devantler.DataMesh.DataProduct.Features.DataIngestion;
using Devantler.DataMesh.DataProduct.Features.DataStore;
using Devantler.DataMesh.DataProduct.Features.Metadata;
using Devantler.DataMesh.DataProduct.Features.Metrics;
using Devantler.DataMesh.DataProduct.Features.Tracing;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct.Features;

/// <summary>
/// Extensions for registering features and configuring the web application to use them.
/// </summary>
public static class FeaturesStartupExtensions
{
    /// <summary>
    /// Registers features to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddFeatures(this WebApplicationBuilder builder)
    {
        var options = builder.Configuration.GetDataProductOptions();

#pragma warning disable S1854
        _ = builder.Services.AddOptions<DataProductOptions>().Configure(o =>
        {
            o.Name = options.Name;
            o.Description = options.Description;
            o.Version = options.Version;
            o.License = options.License;
            o.Owner = options.Owner;
            o.FeatureFlags = options.FeatureFlags;
            o.Apis = options.Apis;
            o.SchemaRegistry = options.SchemaRegistry;
            o.DataStore = options.DataStore;
            o.CacheStore = options.CacheStore;
            o.DataIngestors = options.DataIngestors;
        });
#pragma warning restore S1854

        _ = builder.Services.AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key));
        _ = builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        if (options.FeatureFlags.EnableAuthentication)
            _ = builder.Services.AddAuthentication();

        if (options.FeatureFlags.EnableAuthorisation)
            _ = builder.Services.AddAuthorization();

        _ = builder.Services.AddDataStore(options);

        if (options.FeatureFlags.EnableCaching)
            _ = builder.Services.AddCaching(options);

        if (options.FeatureFlags.EnableDataIngestion)
            _ = builder.Services.AddDataIngestion(options);

        if (options.FeatureFlags.EnableDataEgestion)
            _ = builder.Services.AddDataEgestion();

        if (options.FeatureFlags.EnableMetadata)
            _ = builder.Services.AddMetadata();

        if (options.FeatureFlags.EnableMetrics)
            _ = builder.Services.AddMetrics();

        if (options.FeatureFlags.EnableTracing)
            _ = builder.Services.AddTracing();

        _ = builder.Services.AddApis(options, builder.Environment);
    }

    /// <summary>
    /// Configures the web application to use enabled features.
    /// </summary>
    /// <param name="app"></param>
    public static void UseFeatures(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<DataProductOptions>>().Value;

        if (options.FeatureFlags.EnableAuthentication)
            _ = app.UseAuthentication();

        if (options.FeatureFlags.EnableAuthorisation)
            _ = app.UseAuthorization();

        _ = app.UseDataStore(options);

        if (options.FeatureFlags.EnableMetadata)
            _ = app.UseMetadata();

        if (options.FeatureFlags.EnableMetrics)
            _ = app.UseMetrics();

        if (options.FeatureFlags.EnableTracing)
            _ = app.UseTracing();

        _ = app.UseApis(options);
    }
}
