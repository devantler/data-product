using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Entities;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models;

/// <summary>
/// A model that represents the root DataHub Metadata model. See 
/// </summary>
public class Metadata
{
    /// <summary>
    /// The metadata entities.
    /// </summary>
    public List<IMetadataEntity> Entities { get; set; } = new List<IMetadataEntity>();
}