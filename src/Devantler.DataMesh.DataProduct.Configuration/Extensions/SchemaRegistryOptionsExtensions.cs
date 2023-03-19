using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;

namespace Devantler.DataMesh.DataProduct.Configuration.Extensions;

/// <summary>
/// A static class containing extension methods for the <see cref="ISchemaRegistryOptions"/> type.
/// </summary>
public static class SchemaRegistryOptionsExtensions
{
    /// <summary>
    /// Overrides the local schema registry path.
    /// </summary>
    /// <remarks>
    /// This is a hack to get around the fact that paths only work well with additional files in source generators.
    /// </remarks>
    /// <param name="options"></param>
    /// <param name="path"></param>
    public static void OverrideLocalSchemaRegistryPath(this ISchemaRegistryOptions options, string? path)
    {
        if (options is not LocalSchemaRegistryOptions localSchemaRegistryOptions || localSchemaRegistryOptions.Path.StartsWith("/") || string.IsNullOrEmpty(path))
            return;

        localSchemaRegistryOptions.Path = path;
    }
}
