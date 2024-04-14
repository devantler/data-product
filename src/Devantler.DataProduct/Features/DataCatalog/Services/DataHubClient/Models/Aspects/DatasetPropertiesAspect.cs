using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects;

/// <summary>
/// A model that represents the dataset properties aspect in the DataHub metadata model.
/// </summary>
public class DatasetPropertiesAspect : IMetadataAspect
{
  /// <inheritdoc/>
  [JsonPropertyName("__type")]
  public string Type { get; set; } = "DatasetProperties";

  /// <summary>
  /// The description of the dataset.
  /// </summary>
  public string Description { get; set; } = string.Empty;
}
