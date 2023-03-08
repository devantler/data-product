using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.Apis.GraphQL;
using Devantler.DataMesh.DataProduct.Features.Apis.Rest;
using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct.Features.Apis;

/// <summary>
/// Extensions to registers APIs to the DI container and configure the web application to use them.
/// </summary>
public static class ApisStartupExtensions
{
    /// <summary>
    /// Registers APIs to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="options"></param>
    /// <param name="environment"></param>
    public static WebApplicationBuilder AddApis(this WebApplicationBuilder builder, DataProductOptions options, IWebHostEnvironment environment)
    {
        _ = builder.AddForFeature(nameof(FeatureFlagsOptions.EnableApis), ApiFeatureFlagValues.Rest.ToString(),
            b => b.AddRest(options)
        );

        _ = builder.AddForFeature(nameof(FeatureFlagsOptions.EnableApis), ApiFeatureFlagValues.GraphQL.ToString(),
            b => b.AddGraphQL(environment)
        );

        return builder;
    }

    /// <summary>
    /// Configures the web application to use APIs.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseApis(this WebApplication app, DataProductOptions options)
    {
        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableApis), ApiFeatureFlagValues.Rest.ToString(),
            a => a.UseRest(options)
        );

        _ = app.UseForFeature(nameof(FeatureFlagsOptions.EnableApis), ApiFeatureFlagValues.GraphQL.ToString(),
            a => a.UseGraphQL()
        );

        return app;
    }
}
