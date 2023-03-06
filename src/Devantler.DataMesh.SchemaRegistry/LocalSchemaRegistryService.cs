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
    public async Task<Schema> GetSchemaAsync(string subject, int version, CancellationToken cancellationToken = default)
    {
        string schemaString = await GetSchemaStringAsync(subject, version, cancellationToken);

        var schemaReader = new JsonSchemaReader();

        return schemaReader.Read(schemaString);
    }

    /// <inheritdoc/>
    public Schema GetSchema(string subject, int version)
    {
        string schemaString = GetSchemaString(subject, version);

        var schemaReader = new JsonSchemaReader();

        return schemaReader.Read(schemaString);
    }

    /// <summary>
    /// Gets the schema as a string from the file system.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    private string GetSchemaString(string subject, int version)
    {
        string schemaFileName = $"{subject}-v{version}.avsc";

        string schemaFile = Directory.GetFiles(_schemaRegistryOptions?.Path ?? "schemas", schemaFileName).FirstOrDefault();

        if (string.IsNullOrEmpty(schemaFile))
            throw new FileNotFoundException($"Schema file {schemaFileName} in path {_schemaRegistryOptions?.Path ?? "schemas"} not found.");

        return File.ReadAllText(schemaFile);

    }

    /// <summary>
    /// Gets the schema as a string from the file system asynchronously.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="version"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<string> GetSchemaStringAsync(string subject, int version, CancellationToken cancellationToken)
    {
        string schemaFileName = $"{subject}-v{version}.avsc";

        string schemaFile = Directory.GetFiles(_schemaRegistryOptions?.Path ?? "schemas", schemaFileName).FirstOrDefault();

        if (string.IsNullOrEmpty(schemaFile))
            throw new FileNotFoundException($"Schema file {schemaFileName} in path {_schemaRegistryOptions?.Path ?? "schemas"} not found.");

        return await File.ReadAllTextAsync(schemaFile, cancellationToken);
    }
}
