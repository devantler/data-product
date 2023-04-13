namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata;

/// <summary>
/// A model that represents a field in a schema.
/// </summary>
public class SchemaField
{
    /// <summary>
    /// The name/path of the field.
    /// </summary>
    public required string FieldPath { get; set; }

    /// <summary>
    /// The JSON path of the field.
    /// </summary>
    public string JsonPath { get; set; } = string.Empty;

    /// <summary>
    /// Whether or not the field is nullable.
    /// </summary>
    public bool Nullable { get; set; }

    /// <summary>
    /// The description of the field.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The type of the field.
    /// </summary>
    public required SchemaFieldType Type { get; set; }

    /// <summary>
    /// The native data type of the field.
    /// </summary>
    public string NativeDataType { get; set; } = string.Empty;

    /// <summary>
    /// Whether or not the field is recursive.
    /// </summary>
    public bool Recursive { get; set; }
}