using Devantler.DataMesh.DataProduct.Configuration.Options.CacheStore;

namespace Devantler.DataMesh.DataProduct.Configuration.Extensions;

/// <summary>
/// Extensions for <see cref="ICacheStoreOptions"/>.
/// </summary>
public static class CacheStoreOptionsExtensions
{
    /// <summary>
    /// Gets the expiration time in days.
    /// </summary>
    public static string ToDays(this string expirationTime)
    {
        return string.IsNullOrWhiteSpace(expirationTime)
            ? "0"
            : expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? expirationTime[..^1]
            : expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 24).ToString()
            : expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[^1])
            ? (int.Parse(expirationTime[..^1]) / 24 / 60).ToString()
            : expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 24 / 60 / 60).ToString()
            : throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in hours.
    /// </summary>
    public static string ToHours(this string expirationTime)
    {
        return string.IsNullOrWhiteSpace(expirationTime)
            ? "0"
            : expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) * 24).ToString()
            : expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase)
            ? expirationTime[..^1]
            : expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[^1])
            ? (int.Parse(expirationTime[..^1]) / 60).ToString()
            : expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 60 / 60).ToString()
            : throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in minutes.
    /// </summary>
    public static string ToMonths(this string expirationTime)
    {
        return string.IsNullOrWhiteSpace(expirationTime)
            ? "0"
            : expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 30).ToString()
            : expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 24 / 30).ToString()
            : expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[^1])
            ? (int.Parse(expirationTime[..^1]) / 24 / 60 / 30).ToString()
            : expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 24 / 60 / 60 / 30).ToString()
            : throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in minutes.
    /// </summary>
    public static string ToMinutes(this string expirationTime)
    {
        return string.IsNullOrWhiteSpace(expirationTime)
            ? "0"
            : expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) * 24 * 60).ToString()
            : expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) * 60).ToString()
            : expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[^1])
            ? expirationTime[..^1]
            : expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) / 60).ToString()
            : throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in seconds.
    /// </summary>
    public static string ToSeconds(this string expirationTime)
    {
        return string.IsNullOrWhiteSpace(expirationTime)
            ? "0"
            : expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) * 24 * 60 * 60).ToString()
            : expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase)
            ? (int.Parse(expirationTime[..^1]) * 60 * 60).ToString()
            : expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[^1])
            ? (int.Parse(expirationTime[..^1]) * 60).ToString()
            : expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase)
            ? expirationTime[..^1]
            : throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in a TimeSpan.
    /// </summary>
    public static TimeSpan ToTimeSpan(this string expirationTime)
    {
        return string.IsNullOrWhiteSpace(expirationTime)
            ? TimeSpan.Zero
            : expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase)
            ? TimeSpan.FromDays(int.Parse(expirationTime[..^1]))
            : expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase)
            ? TimeSpan.FromHours(int.Parse(expirationTime[..^1]))
            : expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[^1])
            ? TimeSpan.FromMinutes(int.Parse(expirationTime[..^1]))
            : expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase)
            ? TimeSpan.FromSeconds(int.Parse(expirationTime[..^1]))
            : throw new ArgumentException("Invalid expiration time format.");
    }
}