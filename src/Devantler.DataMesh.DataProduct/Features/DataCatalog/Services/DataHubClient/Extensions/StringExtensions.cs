using Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Models.Aspects.SchemaMetadata;

namespace Devantler.DataMesh.DataProduct.Features.DataCatalog.Services.DataHubClient.Extensions;

/// <summary>
/// Extensions for the <see cref="string"/> class that are specific to the Data Hub client.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts a string to a <see cref="SchemaFieldType"/>.
    /// </summary>
    public static SchemaFieldType ToSchemaFieldType(this string text)
    {
        string typeOnly = text.Split('<').First();
        return typeOnly switch
        {
            "string" => new SchemaFieldType(SchemaFieldDataType.StringType),
            "boolean" => new SchemaFieldType(SchemaFieldDataType.BooleanType),
            "bytes" => new SchemaFieldType(SchemaFieldDataType.BytesType),
            "int" => new SchemaFieldType(SchemaFieldDataType.NumberType),
            "long" => new SchemaFieldType(SchemaFieldDataType.NumberType),
            "float" => new SchemaFieldType(SchemaFieldDataType.NumberType),
            "double" => new SchemaFieldType(SchemaFieldDataType.NumberType),
            "decimal" => new SchemaFieldType(SchemaFieldDataType.NumberType),
            "DateTime" => new SchemaFieldType(SchemaFieldDataType.DateType),
            "IEnumerable" => new SchemaFieldType(SchemaFieldDataType.ArrayType),
            "IList" => new SchemaFieldType(SchemaFieldDataType.ArrayType),
            "List" => new SchemaFieldType(SchemaFieldDataType.ArrayType),
            "Array" => new SchemaFieldType(SchemaFieldDataType.ArrayType),
            "Dictionary" => new SchemaFieldType(SchemaFieldDataType.MapType),
            "object" => new SchemaFieldType(SchemaFieldDataType.RecordType),
            _ => new SchemaFieldType(SchemaFieldDataType.RecordType)
        };
    }
}