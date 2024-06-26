using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.FeatureFlags;
using Devantler.DataProduct.Features.Apis;
using Devantler.DataProduct.Features.Auth;
using Devantler.DataProduct.Features.Caching;
using Devantler.DataProduct.Features.Configuration;
using Devantler.DataProduct.Features.Dashboard;
using Devantler.DataProduct.Features.DataCatalog;
using Devantler.DataProduct.Features.DataStore;
using Devantler.DataProduct.Features.Inputs;
using Devantler.DataProduct.Features.Mapping;
using Devantler.DataProduct.Features.Outputs;
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
  /// <param name="args"></param>
  public static void AddFeatures(this WebApplicationBuilder builder, string[] args)
  {
    var options = builder.AddConfiguration(args);
    _ = builder.Services.AddMapping();
    _ = builder.Services.AddValidation();

    _ = builder.Services.AddFeatureManagement(builder.Configuration.GetSection(FeatureFlagsOptions.Key));

    _ = builder.Services.AddDataStore(options);
    _ = builder.Services.AddSchemaRegistry(options);

    if (options.FeatureFlags.EnableCaching)
      _ = builder.Services.AddCaching(options);

    if (options.FeatureFlags.EnableApis.Count != 0)
      _ = builder.Services.AddApis(options, builder.Environment);

    if (options.FeatureFlags.EnableDashboard)
      _ = builder.AddDashboard();

    if (options.FeatureFlags.EnableAuth)
      _ = builder.Services.AddAuth(options);

    if (options.FeatureFlags.EnableDataCatalog)
      _ = builder.Services.AddDataCatalog(options);

    if (options.FeatureFlags.EnableInputs)
      _ = builder.Services.AddInputs(options);

    if (options.FeatureFlags.EnableOutputs)
      _ = builder.Services.AddOutputs(options);

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

    if (options.FeatureFlags.EnableDataCatalog)
      _ = app.UseDataCatalog();

    if (options.FeatureFlags.EnableApis.Count != 0)
      _ = app.UseApis(options);

    if (options.FeatureFlags.EnableDashboard)
      _ = app.UseDashboard(options);

    if (options.FeatureFlags.EnableAuth)
      _ = app.UseAuth();
  }
}
