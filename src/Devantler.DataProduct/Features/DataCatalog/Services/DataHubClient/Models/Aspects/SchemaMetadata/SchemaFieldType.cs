using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata;

/// <summary>
/// A model that represents a field type in a schema.
/// </summary>
public class SchemaFieldType
{
  /// <summary>
  /// Creates a new instance of the <see cref="SchemaFieldType"/> class.
  /// </summary>
  public SchemaFieldType(SchemaFieldDataType type)
  {
    Type = new ActualSchemaFieldType
    {
      Type = type.ToString()
    };
  }

  /// <summary>
  /// The actual type of the field.
  /// </summary>
  public ActualSchemaFieldType Type { get; set; }
}

/// <summary>
/// The actual type of the field.
/// </summary>
public class ActualSchemaFieldType
{
  /// <summary>
  /// The type of the field.
  /// </summary>
  [JsonPropertyName("__type")]
  public required string Type { get; set; }
}

/// <summary>
/// The different possible field types.
/// </summary>
public enum SchemaFieldDataType
{
  /// <summary>
  /// The field type is a boolean.
  /// </summary>
  BooleanType,
  /// <summary>
  /// The field type is a fixed type.
  /// </summary>
  FixedType,
  /// <summary>
  /// The field type is a string.
  /// </summary>
  StringType,
  /// <summary>
  /// The field type is a bytes type.
  /// </summary>
  BytesType,
  /// <summary>
  /// The field type is a number.
  /// </summary>
  NumberType,
  /// <summary>
  /// The field type is a date.
  /// </summary>
  DateType,
  /// <summary>
  /// The field type is a time.
  /// </summary>
  TimeType,
  /// <summary>
  /// The field type is an enum.
  /// </summary>
  EnumType,
  /// <summary>
  /// The field type is null.
  /// </summary>
  NullType,
  /// <summary>
  /// The field type is a map.
  /// </summary>
  MapType,
  /// <summary>
  /// The field type is an array.
  /// </summary>
  ArrayType,
  /// <summary>
  /// The field type is a union.
  /// </summary>
  UnionType,
  /// <summary>
  /// The field type is a record.
  /// </summary>
  RecordType
}
