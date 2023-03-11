using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct.Features;

/// <summary>
/// Extensions to FeatureManagement, to allow reading feature flags during startup.
/// </summary>
public static class FeatureManagementExtensions
{
    /// <summary>
    /// Conditionally creates a branch in the request pipeline that is rejoined to the main pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="featureName">The feature that is required to be enabled to take use this application branch</param>
    /// <param name="featureValue">The value of the feature that is required to be enabled to take use this application branch</param>
    /// <param name="configuration">Configures a branch to take</param>
    public static IApplicationBuilder UseForFeature(this IApplicationBuilder app, string featureName, string featureValue, Action<IApplicationBuilder> configuration)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        if (string.IsNullOrEmpty(featureName))
        {
            throw new ArgumentNullException(nameof(featureName));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        // Create and configure the branch builder right away; otherwise,
        // we would end up running our branch after all the components
        // that were subsequently added to the main builder.
        var branchBuilder = app.New();

        configuration(branchBuilder);

        return app.Use(main =>
        {
            // This is called only when the main application builder 
            // is built, not per request.
            branchBuilder.Run(main);

            var branch = branchBuilder.Build();

            return async (context) =>
            {
                IFeatureManager fm = context.RequestServices.GetRequiredService<IFeatureManagerSnapshot>();

                var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
                if (configuration.GetSection(FeatureFlagsOptions.Key).GetValue<List<string>>(featureName)?.Contains(featureValue) == true)
                {
                    await branch(context).ConfigureAwait(false);
                }
                else
                {
                    await main(context).ConfigureAwait(false);
                }
            };
        });
    }
}