using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions.Providers;

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
    public static void OverrideLocalSchemaRegistryPath(this ISchemaRegistryOptions options, string hackpath)
    {
        if (options is LocalSchemaRegistryOptions localSchemaRegistryOptions && string.IsNullOrEmpty(localSchemaRegistryOptions.Path))
            localSchemaRegistryOptions.Path = hackpath;
    }
}
