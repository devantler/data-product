using System.Text.RegularExpressions;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value == null ? null :
            Regex.Replace(
                value.ToString()!,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)
            ).ToUpperInvariant();
    }
}
