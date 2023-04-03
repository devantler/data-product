using System.Reflection;

namespace Devantler.DataMesh.DataProduct.Features.Mapping;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/> to add mapping.
/// </summary>
public static class MappingStartupExtensions
{
    /// <summary>
    /// Adds mapping to the DI container.
    /// </summary>
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}