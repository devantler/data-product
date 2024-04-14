using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema that represents the document schema.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlatformDocumentSchema"/> class.
/// </remarks>
public class PlatformDocumentSchema(DocumentSchemaType documentSchemaType) : IPlatformSchema
{

  /// <inheritdoc/>
  [JsonPropertyName("__type")]
  public string Type { get; set; } = documentSchemaType.ToString();

  /// <summary>
  /// The value of the document schema.
  /// </summary>
  public string DocumentSchema { get; set; } = string.Empty;
}
