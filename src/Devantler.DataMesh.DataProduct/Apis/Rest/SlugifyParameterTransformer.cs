using System.Text.RegularExpressions;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// An outbound parameter transformer that converts PascalCase to kebab-case.
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Transforms an outbound parameter from PascalCase to kebab-case.
    /// </summary>
    /// <param name="value"></param>
    public string? TransformOutbound(object? value)
    {
        return value == null ? null :
            Regex.Replace(
                value.ToString()!,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)
            ).ToLower(System.Globalization.CultureInfo.CurrentCulture);
    }
}
