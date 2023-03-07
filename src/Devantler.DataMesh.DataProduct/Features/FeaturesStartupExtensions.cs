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

        _ = builder.Services
            .AddOptions<DataProductOptions>().Bind(builder.Configuration.GetSection(DataProductOptions.Key));

        _ = builder.Services
            .AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key));

        _ = builder.Services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddDataStore(options)
            .AddApis(options, builder.Environment);
        //.AddDataIngestionSources(options);
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

        if (options.FeatureFlags.EnableAuthorization)
            _ = app.UseAuthorization();

        _ = app.UseDataStore(options)
            .UseApis(options);
        //.UseDataIngestionSources(options);
    }
}
