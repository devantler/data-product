using Devantler.Commons.StringHelpers;

namespace Devantler.DataProduct.Features.Apis.Rest;

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
        => (value?.ToString() ?? string.Empty).ToKebabCase();
}
