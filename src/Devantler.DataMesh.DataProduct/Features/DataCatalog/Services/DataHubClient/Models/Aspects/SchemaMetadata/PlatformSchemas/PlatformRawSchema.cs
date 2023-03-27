using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema that represents the raw schema.
/// </summary>
public class PlatformRawSchema : IPlatformSchema
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformRawSchema"/> class.
    /// </summary>
    public PlatformRawSchema(RawSchemaType rawSchemaType)
        => Type = rawSchemaType.ToString();

    /// <inheritdoc/>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// The value of the raw schema.
    /// </summary>
    public string RawSchema { get; set; } = string.Empty;
}