using System.Text.Json.Serialization;
using Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects;

/// <summary>
/// An interface that represents an aspect in the DataHub metadata model.
/// </summary>
[JsonDerivedType(typeof(SchemaMetadataAspect))]
[JsonDerivedType(typeof(DatasetPropertiesAspect))]
[JsonDerivedType(typeof(InstitutionalMemoryAspect))]
public interface IMetadataAspect
{
    /// <summary>
    /// The aspect type.
    /// </summary>
    public string Type { get; set; }
}