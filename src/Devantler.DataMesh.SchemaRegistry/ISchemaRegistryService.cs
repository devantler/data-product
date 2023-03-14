using Chr.Avro.Abstract;

namespace Devantler.DataMesh.SchemaRegistry;

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
    /// <param name="cancellationToken"></param>
    public Task<Schema> GetSchemaAsync(string subject, int version, CancellationToken cancellationToken = default);

    /// <summary>
    /// Abstract method to retrieve a schema from a schema registry.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="version"></param>
    public Schema GetSchema(string subject, int version);
}
