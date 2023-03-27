using Chr.Avro.Abstract;

namespace Devantler.DataMesh.SchemaRegistryClient;

/// <summary>
/// An interface for scheme registry clients.
/// </summary>
public interface ISchemaRegistryClient
{
    /// <summary>
    /// Retrieves a schema from a schema registry.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="version"></param>
    /// <param name="cancellationToken"></param>
    public Task<Schema> GetSchemaAsync(string subject, int version, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a schema from a schema registry.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="version"></param>
    public Schema GetSchema(string subject, int version);
}
