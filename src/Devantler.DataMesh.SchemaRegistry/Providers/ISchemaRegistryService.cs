using Avro;

namespace Devantler.DataMesh.SchemaRegistry.Providers;

/// <summary>
/// An interface for scheme registry services.
/// </summary>
public interface ISchemaRegistryService
{
    /// <summary>
    /// Abstract method to retrieve a schema from a schema registry.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public Task<Schema> GetSchemaAsync(string subject, int version);
}
