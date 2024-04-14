using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema that represents the raw schema.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlatformRawSchema"/> class.
/// </remarks>
public class PlatformRawSchema(RawSchemaType rawSchemaType) : IPlatformSchema
{

  /// <inheritdoc/>
  [JsonPropertyName("__type")]
  public string Type { get; set; } = rawSchemaType.ToString();

  /// <summary>
  /// The value of the raw schema.
  /// </summary>
  public string RawSchema { get; set; } = string.Empty;
}
