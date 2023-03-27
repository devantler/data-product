using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Schemas.PlatformSchemas;

/// <summary>
/// A schema that represents the table schema of a platform.
/// </summary>
public class PlatformTableSchema
{
    /// <summary>
    /// The type of the schema.
    /// </summary>
    [JsonPropertyName("__type")]
    public string Type { get; set; } = "MySqlDDL"; //TODO: change this to something that makes sense.
    /// <summary>
    /// The property that informs DataHub that this is a table schema.
    /// </summary>
    public string TableSchema { get; set; } = "schema";
}