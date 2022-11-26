using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Models;

public class Schema
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("properties")]
    public List<Property> Properties { get; set; } = new();
}
