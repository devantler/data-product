using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Devantler.DataProduct.Features.Inputs.JsonConverters;

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
    switch (reader.TokenType)
    {
      case JsonTokenType.Number:
        return reader.TryGetInt64(out long @long)
            ? @long.ToString(CultureInfo.InvariantCulture)
            : reader.GetDouble().ToString(CultureInfo.InvariantCulture);
      case JsonTokenType.String:
        return reader.GetString();
      case JsonTokenType.None:
      case JsonTokenType.StartObject:
      case JsonTokenType.EndObject:
      case JsonTokenType.StartArray:
      case JsonTokenType.EndArray:
      case JsonTokenType.PropertyName:
      case JsonTokenType.Comment:
      case JsonTokenType.True:
      case JsonTokenType.False:
      case JsonTokenType.Null:
      default:
        {
          using var document = JsonDocument.ParseValue(ref reader);
          return document.RootElement.Clone().ToString();
        }
    }
  }

  /// <inheritdoc/>
  public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
      => writer.WriteStringValue(value.ToString());
}
