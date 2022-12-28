using Avro;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration;

namespace Devantler.DataMesh.SchemaRegistry;

public class LocalSchemaRegistryService : ISchemaRegistryService
{
    private readonly SchemaRegistryOptions _schemaRegistryOptions;

    public LocalSchemaRegistryService(SchemaRegistryOptions schemaRegistryOptions)
    {
        _schemaRegistryOptions = schemaRegistryOptions;
    }

    public Schema GetSchema(string subject, int version)
    {
        if (_schemaRegistryOptions.Path == null)
            throw new InvalidOperationException("Schema registry path not set");

        var schemaFileName = $"{subject.ToCamelCase()}-v{version}.avsc";
        var schemaFile = Directory.GetFiles(_schemaRegistryOptions.Path, schemaFileName);

        if (schemaFile.Length == 0)
            throw new FileNotFoundException($"Schema file not found for {schemaFileName}");

        var schemaString = File.ReadAllText(schemaFile[0]);

        return Schema.Parse(schemaString);
    }
}
