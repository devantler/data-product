using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema that represents the document schema.
/// </summary>
public class PlatformDocumentSchema : IPlatformSchema
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformDocumentSchema"/> class.
    /// </summary>
    public PlatformDocumentSchema(DocumentSchemaType documentSchemaType)
        => Type = documentSchemaType.ToString();

    /// <inheritdoc/>
    [JsonPropertyName("__type")]
    public string Type { get; set; }

    /// <summary>
    /// The value of the document schema.
    /// </summary>
    public string DocumentSchema { get; set; } = string.Empty;
}