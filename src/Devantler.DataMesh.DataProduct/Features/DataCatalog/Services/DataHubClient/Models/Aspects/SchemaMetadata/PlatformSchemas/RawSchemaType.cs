namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// The supported Raw Schema Types.
/// </summary>
public enum RawSchemaType
{
    /// <summary>
    /// The raw schema type for Presto.
    /// </summary>
    PrestoDDL,
    /// <summary>
    /// The raw schema type for anything unknown.
    /// </summary>
    OtherSchema
}