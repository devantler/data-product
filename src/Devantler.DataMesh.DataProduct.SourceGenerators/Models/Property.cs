using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Models;

public class Property
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}
