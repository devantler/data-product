using Avro;

namespace Devantler.DataMesh.SchemaRegistry.Providers.Local;

public class LocalSchemaRegistryService : ISchemaRegistryService
{
    private readonly LocalSchemaRegistryOptions _schemaRegistryOptions;

    public LocalSchemaRegistryService(LocalSchemaRegistryOptions schemaRegistryOptions)
    {
        _schemaRegistryOptions = schemaRegistryOptions;
    }

    public async Task<Schema> GetSchemaAsync(string subject, int version)
    {
        if (_schemaRegistryOptions.Path == null)
            throw new InvalidOperationException("Schema registry path not set");

        var schemaFileName = $"{subject}-v{version}.avsc";
        var schemaFile = Directory.GetFiles(_schemaRegistryOptions.Path, schemaFileName);

        if (schemaFile.Length == 0)
            throw new FileNotFoundException($"Schema file not found for {Directory.GetCurrentDirectory()}/{_schemaRegistryOptions.Path}{schemaFileName}");

        var schemaString = await File.ReadAllTextAsync(schemaFile[0]);

        return Schema.Parse(schemaString);
    }
}
