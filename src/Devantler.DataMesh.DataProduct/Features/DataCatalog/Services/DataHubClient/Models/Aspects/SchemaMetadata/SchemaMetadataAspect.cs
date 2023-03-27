using System.Text.Json.Serialization;
using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata;

/// <summary>
/// A model that represents the schema metadata aspect in the DataHub metadata model.
/// </summary>
public class SchemaMetadataAspect : IMetadataAspect
{
    /// <inheritdoc/>
    [JsonPropertyName("__type")]
    public string Type { get; set; } = "SchemaMetadata";

    /// <summary>
    /// The version of the schema.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// The name of the schema.
    /// </summary>
    public required string SchemaName { get; set; }

    /// <summary>
    /// The platform of that the schema is associated with.
    /// </summary>
    public string Platform { get; set; } = "urn:li:dataPlatform:dataProduct";

    /// <summary>
    /// The platforms schema.
    /// </summary>
    public required IPlatformSchema PlatformSchema { get; set; }

    /// <summary>
    /// The hash of the schema.
    /// </summary>
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// The fields of the schema.
    /// </summary>
    public required List<SchemaField> Fields { get; set; }
}