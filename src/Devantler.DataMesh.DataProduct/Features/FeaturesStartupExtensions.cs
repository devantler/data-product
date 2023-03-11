using System.Reflection;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Features.Apis;
using Devantler.DataMesh.DataProduct.Features.DataStore;
using Microsoft.Extensions.Options;
using Devantler.DataMesh.DataProduct.Features.DataIngestion;
using Devantler.DataMesh.DataProduct.Features.DataEgestion;
using Devantler.DataMesh.DataProduct.Features.Tracing;
using Devantler.DataMesh.DataProduct.Features.Metadata;
using Devantler.DataMesh.DataProduct.Features.Metrics;
using Devantler.DataMesh.DataProduct.Features.Caching;

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
        _ = builder.Services.AddCaching(options);

        _ = builder.Services.AddDataIngestion(options);
        _ = builder.Services.AddDataEgestion(options);

        _ = builder.Services.AddMetadata(options);
        _ = builder.Services.AddMetrics(options);
        _ = builder.Services.AddTracing(options);

        _ = builder.Services.AddApis(options, builder.Environment);
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

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableAuthorisation),
            a => a.UseAuthorization()
        );

        _ = app.UseDataStore(options);

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableCaching),
            a => a.UseCaching(options)
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableDataIngestion),
            a => a.UseDataIngestion(options)
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableDataEgestion),
            a => a.UseDataEgestion(options)
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableMetadata),
            a => a.UseMetadata(options)
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableMetrics),
            a => a.UseMetrics(options)
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableTracing),
            a => a.UseTracing(options)
        );

        _ = app.UseApis(options);
    }
}
