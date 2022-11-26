using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Models;

public class Configuration
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("owner")]
    public Owner Owner { get; set; } = new();

    [JsonPropertyName("schemas")]
    public List<Schema> Schemas { get; set; } = new();

    [JsonPropertyName("features")]
    public Features Features { get; set; } = new();
}
