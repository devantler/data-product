using Avro;

namespace Devantler.DataMesh.SchemaRegistry;

public interface ISchemaRegistryService
{
    public Schema GetSchema(string subject, int version);
}
