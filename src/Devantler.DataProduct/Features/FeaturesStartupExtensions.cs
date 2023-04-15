using Devantler.DataProduct.Core.Configuration.Options;
using Devantler.DataProduct.Core.Configuration.Options.FeatureFlags;
using Devantler.DataProduct.Features.Apis;
using Devantler.DataProduct.Features.Caching;
using Devantler.DataProduct.Features.Dashboard;
using Devantler.DataProduct.Features.DataCatalog;
using Devantler.DataProduct.Features.DataEgestion;
using Devantler.DataProduct.Features.DataIngestion;
using Devantler.DataProduct.Features.DataStore;
using Devantler.DataProduct.Features.SchemaRegistry;
using Devantler.DataProduct.Features.Telemetry;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Devantler.DataProduct.Features;

/// <summary>
/// Extensions for registering features and configuring the web application to use them.
/// </summary>
public static class FeaturesStartupExtensions
{
    /// <summary>
    /// Registers features to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="options"></param>
    public static void AddFeatures(this WebApplicationBuilder builder, DataProductOptions options)
    {
        _ = builder.Services.AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key));

        _ = builder.Services.AddDataStore(options);
        _ = builder.Services.AddSchemaRegistry(options);

        if (options.FeatureFlags.EnableApis.Any())
            _ = builder.Services.AddApis(options, builder.Environment);

        if (options.FeatureFlags.EnableAuthentication)
            _ = builder.Services.AddAuthentication();

        if (options.FeatureFlags.EnableAuthorisation)
            _ = builder.Services.AddAuthorization();

        if (options.FeatureFlags.EnableCaching)
            _ = builder.Services.AddCaching(options);

        if (options.FeatureFlags.EnableDashboard)
            _ = builder.AddDashboard();

        if (options.FeatureFlags.EnableDataCatalog)
            _ = builder.Services.AddDataCatalog(options);

        if (options.FeatureFlags.EnableDataEgestion)
            _ = builder.Services.AddDataEgestion();

        if (options.FeatureFlags.EnableDataIngestion)
            _ = builder.Services.AddDataIngestion(options);

        if (options.FeatureFlags.EnableTelemetry)
            _ = builder.AddTelemetry(options);
    }

    /// <summary>
    /// Configures the web application to use enabled features.
    /// </summary>
    /// <param name="app"></param>
    public static void UseFeatures(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<DataProductOptions>>().Value;

        _ = app.UseDataStore(options);

        if (options.FeatureFlags.EnableAuthentication)
            _ = app.UseAuthentication();

        if (options.FeatureFlags.EnableAuthorisation)
            _ = app.UseAuthorization();

        if (options.FeatureFlags.EnableDataCatalog)
            _ = app.UseDataCatalog();

        if (options.FeatureFlags.EnableApis.Any())
            _ = app.UseApis(options);

        if (options.FeatureFlags.EnableDashboard)
            _ = app.UseDashboard(options);
    }
}
