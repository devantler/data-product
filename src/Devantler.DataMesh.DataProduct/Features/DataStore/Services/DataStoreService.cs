using AutoMapper;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.Caching.Extensions;
using Devantler.DataMesh.DataProduct.Features.Caching.Services;
using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;
using Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Services;

/// <summary>
/// Generic interface for data store services.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class DataStoreService<TKey, TSchema, TEntity> : IDataStoreService<TKey, TSchema>
    where TKey : notnull
    where TSchema : class, Schemas.ISchema<TKey>
    where TEntity : class, IEntity<TKey>
{
    readonly DataProductOptions _options;
    readonly ICacheStoreService<TEntity>? _cacheStore;
    readonly IRepository<TKey, TEntity> _repository;
    readonly IMapper _mapper;

    /// <summary>
    /// Constructs a new instance of <see cref="DataStoreService{TKey,TSchema, TEntity}"/>, and injects the required dependencies.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="mapper"></param>
    protected DataStoreService(IRepository<TKey, TEntity> repository, IServiceProvider serviceProvider, IMapper mapper)
    {
        _options = serviceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
        if (_options.FeatureFlags.EnableCaching)
            _cacheStore = serviceProvider.GetRequiredService<ICacheStoreService<TEntity>>();

        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<TSchema> CreateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        var result = await _repository.CreateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> CreateMultipleAsync(IEnumerable<TSchema> models, bool insertIfNotExists,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        var result = await _repository.CreateMultipleAsync(entities, insertIfNotExists, cancellationToken);

        return _mapper.Map<IEnumerable<TSchema>>(result);
    }

    /// <inheritdoc/>
    public async Task DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteSingleAsync(id, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            string cacheKey = $"{typeof(TEntity).Name}:{id}";
            await _cacheStore.RemoveAsync(cacheKey, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public async Task DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteMultipleAsync(ids, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            foreach (var id in ids)
            {
                string cacheKey = $"{typeof(TEntity).Name}:{id}";
                await _cacheStore.RemoveAsync(cacheKey, cancellationToken);
            }
        }
    }

    /// <inheritdoc/>
    public async Task<TSchema> GetSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            string cacheKey = $"{typeof(TEntity).Name}:{id}";
            var entity = await _cacheStore.GetOrSetAsync(cacheKey, async () => await _repository.ReadSingleAsync(id, cancellationToken), cancellationToken);
            return _mapper.Map<TSchema>(entity);
        }
        else
        {
            var entity = await _repository.ReadSingleAsync(id, cancellationToken);
            return _mapper.Map<TSchema>(entity);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TSchema>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            var ids = await _repository.ReadAllIdsAsync(cancellationToken);

            return await GetMultipleAsync(ids, cancellationToken);
        }
        else
        {
            var entities = await _repository.ReadAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<TSchema>>(entities);
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithLimitAsync(int limit, int offset, CancellationToken cancellationToken = default)
    {
        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            var ids = await _repository.ReadMultipleIdsWithLimitAsync(limit, offset, cancellationToken);

            return await GetMultipleAsync(ids, cancellationToken);
        }
        else
        {
            var entities = await _repository.ReadMultipleWithLimitAsync(limit, offset, cancellationToken);
            return _mapper.Map<IEnumerable<TSchema>>(entities);
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            var cacheKeys = ids.Select(id => $"{typeof(TEntity).Name}:{id}");
            var cachedEntities = await _cacheStore.GetAsync(cacheKeys, cancellationToken);
            var entitiesFromCache = cachedEntities.Where(entity => entity is not null).Select(entity => entity!);

            var idsNotInCache = ids.Except(entitiesFromCache.Select(entity => entity.Id));

            var entitiesNotInCache = Enumerable.Empty<TEntity>();
            if (idsNotInCache.Any())
            {
                entitiesNotInCache = await _repository.ReadMultipleAsync(idsNotInCache, cancellationToken);
                await _cacheStore.SetAsync(idsNotInCache.Select(id => $"{typeof(TEntity).Name}:{id}"), entitiesNotInCache, cancellationToken);
            }

            var entities = entitiesFromCache.Concat(entitiesNotInCache);
            return _mapper.Map<IEnumerable<TSchema>>(entities);
        }
        else
        {
            var entities = await _repository.ReadMultipleAsync(ids, cancellationToken);
            return _mapper.Map<IEnumerable<TSchema>>(entities);
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            var ids = await _repository.ReadMultipleIdsWithPaginationAsync(page, pageSize, cancellationToken);
            return await GetMultipleAsync(ids, cancellationToken);
        }
        else
        {
            var entities = await _repository.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken);
            return _mapper.Map<IEnumerable<TSchema>>(entities);
        }
    }

    /// <inheritdoc/>
    public async Task UpdateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        await _repository.UpdateSingleAsync(entity, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            await _cacheStore.RemoveAsync(entity.CreateCacheKey(), cancellationToken);
        }
    }

    /// <inheritdoc/>
    public async Task UpdateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        await _repository.UpdateMultipleAsync(entities, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            foreach (var entity in entities)
            {
                await _cacheStore.RemoveAsync(entity.CreateCacheKey(), cancellationToken);
            }
        }
    }
}