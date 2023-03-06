using System.Reflection;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Configuration.Extensions;
using Devantler.DataMesh.DataProduct.Features.DataSource;
using Devantler.DataMesh.DataProduct.Features.Apis;
using Devantler.DataMesh.DataProduct.Features.DataStore;

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
        _ = builder.Services
            .AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key)).Services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddDataStore(options)
            .AddApis(options, builder.Environment)
            .AddDataSources(options);
    }

    /// <summary>
    /// Configures the web application to use enabled features.
    /// </summary>
    /// <param name="app"></param>
    public static void UseFeatures(this WebApplication app)
    {
        var options = app.Configuration.GetDataProductOptions();
        _ = app.UseDataStore(options)
            .UseApis(options)
            .UseDataSources(options);
    }
}
