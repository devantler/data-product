using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Entities;

/// <summary>
/// An interface that represents an entity in the DataHub metadata model.
/// </summary>
[JsonDerivedType(typeof(DatasetEntity))]
public interface IMetadataEntity
{
    /// <summary>
    /// The entity urn.
    /// </summary>
    public string EntityUrn { get; set; }

    /// <summary>
    /// The entity type.
    /// </summary>
    public string EntityType { get; set; }
}