using System.Reflection;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Features.Apis;
using Devantler.DataMesh.DataProduct.Features.DataStore;
using Microsoft.Extensions.Options;

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
        var options = builder.Configuration.GetSection(DataProductOptions.Key).Get<DataProductOptions>()
            ?? throw new InvalidOperationException(
                $"Failed to bind configuration section '{DataProductOptions.Key}' to the type '{typeof(DataProductOptions).FullName}'."
            );

        _ = builder.Services.AddOptions<DataProductOptions>().Bind(builder.Configuration.GetSection(DataProductOptions.Key));
        _ = builder.Services.AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key));
        _ = builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        _ = builder.Services.AddAuthentication();
        _ = builder.Services.AddAuthorization();

        _ = builder.Services.AddDataStore(options);
        _ = builder.Services.AddCacheStore(options);

        _ = builder.Services.AddApis(options, builder.Environment);

        _ = builder.Services.AddDataIngestionSources(options);
        _ = builder.Services.AddDataEgestionTargets(options);
        _ = builder.Services.AddMetadataEgestionTargets(options);
        _ = builder.Services.AddTracingEgestionTargets(options);
        _ = builder.Services.AddMetricsEgestionTargets(options);
    }

    /// <summary>
    /// Configures the web application to use enabled features.
    /// </summary>
    /// <param name="app"></param>
    public static void UseFeatures(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<DataProductOptions>>().Value;

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableAuthentication),
            a => a.UseAuthentication()
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableAuthorization),
            a => a.UseAuthorization()
        );

        _ = app.UseDataStore(options)
            .UseApis(options);
        //.UseDataIngestionSources(options);
    }
}
