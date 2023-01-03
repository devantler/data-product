using Avro;

namespace Devantler.DataMesh.SchemaRegistry.Providers;

public interface ISchemaRegistryService
{
    public Task<Schema> GetSchemaAsync(string subject, int version);
}
