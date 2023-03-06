using Chr.Avro.Abstract;
using Chr.Avro.Representation;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions.Providers;

namespace Devantler.DataMesh.SchemaRegistry;

/// <summary>
/// A Local schema registry service.
/// </summary>
public class LocalSchemaRegistryService : ISchemaRegistryService
{
    readonly LocalSchemaRegistryOptions? _schemaRegistryOptions;

    /// <summary>
    /// A constructor to construct a Local schema registry service.
    /// </summary>
    /// <param name="schemaRegistryOptions"></param>
    public LocalSchemaRegistryService(LocalSchemaRegistryOptions? schemaRegistryOptions) => _schemaRegistryOptions = schemaRegistryOptions;

    /// <inheritdoc/>
    public async Task<Schema> GetSchemaAsync(string subject, int version)
        => await GetSchemaImplementation(subject, version);

    /// <inheritdoc/>
    public Schema GetSchema(string subject, int version)
        => GetSchemaImplementation(subject, version).Result;

    async Task<Schema> GetSchemaImplementation(string subject, int version)
    {
        string schemaFileName = $"{subject}-v{version}.avsc";

        string schemaFile = Directory.GetFiles(_schemaRegistryOptions?.Path ?? "schemas", schemaFileName).FirstOrDefault();

        if (string.IsNullOrEmpty(schemaFile))
            throw new FileNotFoundException($"Schema file {schemaFileName} in path {_schemaRegistryOptions?.Path ?? "schemas"} not found.");

        string schemaString = await File.ReadAllTextAsync(schemaFile);

        var schemaReader = new JsonSchemaReader();

        return schemaReader.Read(schemaString);
    }
}
