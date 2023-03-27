using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// A platform schema.
/// </summary>
[JsonDerivedType(typeof(PlatformDocumentSchema))]
[JsonDerivedType(typeof(PlatformRawSchema))]
[JsonDerivedType(typeof(PlatformTableSchema))]
public interface IPlatformSchema
{
    /// <summary>
    /// The type of the platform schema.
    /// </summary>
    public string Type { get; set; }
}