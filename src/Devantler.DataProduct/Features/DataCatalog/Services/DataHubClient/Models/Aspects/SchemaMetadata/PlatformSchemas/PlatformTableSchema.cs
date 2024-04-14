using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema that represents the table schema.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlatformTableSchema"/> class.
/// </remarks>
public class PlatformTableSchema(TableSchemaType tableSchemaType) : IPlatformSchema
{

  /// <inheritdoc/>
  [JsonPropertyName("__type")]
  public string Type { get; set; } = tableSchemaType.ToString();

  /// <summary>
  /// The value of the table schema.
  /// </summary>
  public string TableSchema { get; set; } = string.Empty;
}
