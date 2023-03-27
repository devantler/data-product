namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata.PlatformSchemas;

/// <summary>
/// The supported Table Schema Types.
/// </summary>
public enum TableSchemaType
{
    /// <summary>
    /// The table schema type for MySQL.
    /// </summary>
    MySqlDDL,
    /// <summary>
    /// The table schema type for Espresso.
    /// </summary>
    EspressoSchema,
    /// <summary>
    /// The table schema type for Oracle.
    /// </summary>
    OracleDDL
}