using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema that represents the table schema.
/// </summary>
public class PlatformTableSchema : IPlatformSchema
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformTableSchema"/> class.
    /// </summary>
    public PlatformTableSchema(TableSchemaType tableSchemaType)
        => Type = tableSchemaType.ToString();

    /// <inheritdoc/>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// The value of the table schema.
    /// </summary>
    public string TableSchema { get; set; } = string.Empty;
}