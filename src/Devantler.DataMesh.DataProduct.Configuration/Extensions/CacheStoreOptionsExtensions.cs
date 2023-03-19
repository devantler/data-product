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
        if (string.IsNullOrWhiteSpace(expirationTime))
        {
            return "0";
        }
        if (expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            return expirationTime.Substring(0, expirationTime.Length - 1);
        }
        if (expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 24).ToString();
        }
        if (expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[expirationTime.Length - 1]))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 24 / 60).ToString();
        }
        if (expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 24 / 60 / 60).ToString();
        }
        throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in hours.
    /// </summary>
    public static string ToHours(this string expirationTime)
    {
        if (string.IsNullOrWhiteSpace(expirationTime))
        {
            return "0";
        }
        if (expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) * 24).ToString();
        }
        if (expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase))
        {
            return expirationTime.Substring(0, expirationTime.Length - 1);
        }
        if (expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[expirationTime.Length - 1]))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 60).ToString();
        }
        if (expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 60 / 60).ToString();
        }
        throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in minutes.
    /// </summary>
    public static string ToMonths(this string expirationTime)
    {
        if (string.IsNullOrWhiteSpace(expirationTime))
        {
            return "0";
        }
        if (expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 30).ToString();
        }
        if (expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 24 / 30).ToString();
        }
        if (expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[expirationTime.Length - 1]))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 24 / 60 / 30).ToString();
        }
        if (expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 24 / 60 / 60 / 30).ToString();
        }
        throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in minutes.
    /// </summary>
    public static string ToMinutes(this string expirationTime)
    {
        if (string.IsNullOrWhiteSpace(expirationTime))
        {
            return "0";
        }
        if (expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) * 24 * 60).ToString();
        }
        if (expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) * 60).ToString();
        }
        if (expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[expirationTime.Length - 1]))
        {
            return expirationTime.Substring(0, expirationTime.Length - 1);
        }
        if (expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) / 60).ToString();
        }
        throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in seconds.
    /// </summary>
    public static string ToSeconds(this string expirationTime)
    {
        if (string.IsNullOrWhiteSpace(expirationTime))
        {
            return "0";
        }
        if (expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) * 24 * 60 * 60).ToString();
        }
        if (expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) * 60 * 60).ToString();
        }
        if (expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[expirationTime.Length - 1]))
        {
            return (int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)) * 60).ToString();
        }
        if (expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return expirationTime.Substring(0, expirationTime.Length - 1);
        }
        throw new ArgumentException("Invalid expiration time format.");
    }

    /// <summary>
    /// Gets the expiration time in a TimeSpan.
    /// </summary>
    public static TimeSpan ToTimeSpan(this string expirationTime)
    {
        if (string.IsNullOrWhiteSpace(expirationTime))
        {
            return TimeSpan.Zero;
        }
        if (expirationTime.EndsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            return TimeSpan.FromDays(int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)));
        }
        if (expirationTime.EndsWith("h", StringComparison.OrdinalIgnoreCase))
        {
            return TimeSpan.FromHours(int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)));
        }
        if (expirationTime.EndsWith("m", StringComparison.OrdinalIgnoreCase) || char.IsNumber(expirationTime[expirationTime.Length - 1]))
        {
            return TimeSpan.FromMinutes(int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)));
        }
        if (expirationTime.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return TimeSpan.FromSeconds(int.Parse(expirationTime.Substring(0, expirationTime.Length - 1)));
        }
        throw new ArgumentException("Invalid expiration time format.");
    }
}