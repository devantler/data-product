using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects;

/// <summary>
/// A model that represents the institutional memory aspect in the DataHub metadata model.
/// </summary>
public class InstitutionalMemoryAspect : IMetadataAspect
{
    /// <inheritdoc/>
    [JsonPropertyName("__type")]
    public string Type { get; set; } = "InstitutionalMemory";

    /// <summary>
    /// Institutional memory metadata elements
    /// </summary>
    public List<InstitutionalMemoryMetadata> Elements { get; set; } = new();

}

/// <summary>
/// A model that represents the institutional memory metadata in the DataHub metadata model.
/// </summary>
public class InstitutionalMemoryMetadata
{
    /// <summary>
    /// A link that provides more information about the dataset.
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// A description of the link.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Audit information for who and when the link was created.
    /// </summary>
    public AuditStamp CreateStamp { get; set; } = new();
}

/// <summary>
/// A stamp for who and when an action was performed.
/// </summary>
public class AuditStamp
{
    /// <summary>
    /// The time the action was performed.
    /// </summary>
    public long Time { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    /// <summary>
    /// The actor that performed the action.
    /// </summary>
    public string Actor { get; set; } = "urn:li:corpuser:system";
}