using System.Text;

namespace Devantler.DataProduct.Generator.Extensions;

/// <summary>
/// A static class containing extension methods for the <see cref="string"/> type.
/// </summary>
public static class StringExtensions
{
  /// <summary>
  /// Adds metadata to the generated code.
  /// </summary>
  /// <param name="text"></param>
  /// <param name="executingGeneratorType"></param>
  public static string AddMetadata(this string text, Type executingGeneratorType)
  {
    StringBuilder sb = new();
    _ = sb.AppendLine("// <auto-generated>")
        .Append("// This code was generated by: '").Append(executingGeneratorType.FullName).AppendLine("'.")
        .AppendLine("// Any changes made to this file will be overwritten.")
        .Append(text);
    return sb.ToString();
  }
}
