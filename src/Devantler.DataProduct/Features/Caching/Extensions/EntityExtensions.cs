using Devantler.DataProduct.Features.DataStore.Entities;

namespace Devantler.DataProduct.Features.Caching.Extensions;

/// <summary>
/// Extensions for <see cref="IEntity{T}"/>.
/// </summary>
public static class EntityExtensions
{
    /// <summary>
    /// Creates a cache key for the given entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static string CreateCacheKey<T>(this IEntity<T> entity)
        => $"{entity.GetType().Name}:{entity.Id}";
}