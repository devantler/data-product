using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Devantler.DataMesh.DataProduct.JsonConverters;

/// <summary>
/// A converter that converts numbers to strings.
/// </summary>
public class NumberToStringConverter : JsonConverter<object>
{
    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
       => typeof(string) == typeToConvert;

    /// <inheritdoc/>
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.TryGetInt64(out long @long)
                ? @long.ToString(CultureInfo.InvariantCulture)
                : reader.GetDouble().ToString(CultureInfo.InvariantCulture);
        }
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString();
        }
        using var document = JsonDocument.ParseValue(ref reader);
        return document.RootElement.Clone().ToString();
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
       => writer.WriteStringValue(value.ToString());
}