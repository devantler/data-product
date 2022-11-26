using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Models;

public class Features
{
    [JsonPropertyName("apis")]
    public List<string> Apis { get; set; } = new();

    [JsonPropertyName("database")]
    public Database Database { get; set; } = Database.Auto;

    [JsonPropertyName("caching")]
    public bool Caching { get; set; } = false;

    [JsonPropertyName("metadata")]
    public bool Metadata { get; set; } = false;

    [JsonPropertyName("authentication")]
    public bool Authentication { get; set; } = false;

    [JsonPropertyName("authorisation")]
    public bool Authorisation { get; set; } = false;

    [JsonPropertyName("metrics")]
    public bool Metrics { get; set; } = false;

    [JsonPropertyName("tracing")]
    public bool Tracing { get; set; } = false;

    [JsonPropertyName("logging")]
    public bool Logging { get; set; } = false;

    [JsonPropertyName("health")]
    public bool Health { get; set; } = false;
}
