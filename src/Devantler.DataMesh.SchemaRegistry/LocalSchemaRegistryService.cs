using Chr.Avro.Abstract;
using Chr.Avro.Representation;
using Devantler.Commons.StringHelpers;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistryOptions.Providers;

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
    {
        string schemaFileName = $"{subject.ToKebabCase()}-v{version}.avsc";

        string[] schemaFile = Directory.GetFiles(_schemaRegistryOptions?.Path ?? "Schemas", schemaFileName);

        string schemaString = await File.ReadAllTextAsync(schemaFile[0]);

        var schemaReader = new JsonSchemaReader();

        return schemaReader.Read(schemaString);
    }
}
