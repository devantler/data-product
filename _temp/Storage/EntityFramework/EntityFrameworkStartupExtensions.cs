using Devantler.DataMesh.DataProduct.Core.Extensions;

namespace Devantler.DataMesh.DataProduct.Storage.EntityFramework;

public static partial class EntityFrameworkStartupExtensions
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.ENTITY_FRAMEWORK_FEATURE_FLAG)) return services;

        AddFromSourceGenerator(services);
        return services;
    }

    static partial void AddFromSourceGenerator(IServiceCollection services);
}
