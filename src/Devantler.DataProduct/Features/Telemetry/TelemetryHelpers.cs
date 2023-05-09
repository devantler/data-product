using System.Reflection;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;

namespace Devantler.DataProduct.Features.Telemetry;

/// <summary>
/// Helper methods for telemetry.
/// </summary>
public static class TelemetryHelpers
{
    /// <summary>
    /// The process attributes that are added to all telemetry.
    /// </summary>
    public static Dictionary<string, object> GetProcessAttributes(DataProductOptions options)
    {
        return new Dictionary<string, object>
        {
            ["environment"] = options.Environment,
            ["service"] = options.Name.ToKebabCase(),
            ["version"] = options.Release,
            ["assembly"] = Assembly.GetExecutingAssembly().GetName().FullName
        };
    }
}