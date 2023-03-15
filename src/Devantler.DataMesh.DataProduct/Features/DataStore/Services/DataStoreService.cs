using AutoMapper;
using AutoMapper.QueryableExtensions;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Features.Caching.Services;
using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;
using Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;
using Microsoft.Extensions.Options;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Services;

/// <summary>
/// Generic interface for data store services.
/// </summary>
/// <typeparam name="TSchema"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class DataStoreService<TSchema, TEntity> : IDataStoreService<TSchema>
    where TSchema : class
    where TEntity : class, IEntity
{
    readonly DataProductOptions _options;
    readonly ICacheStoreService<string, TEntity>? _cacheStore;
    readonly IRepository<TEntity> _repository;
    readonly IMapper _mapper;

    /// <summary>
    /// Constructs a new instance of <see cref="DataStoreService{TSchema, TEntity}"/>, and injects the required dependencies.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="mapper"></param>
    protected DataStoreService(IRepository<TEntity> repository, IServiceProvider serviceProvider, IMapper mapper)
    {
        _options = serviceProvider.GetRequiredService<IOptions<DataProductOptions>>().Value;
        if (_options.FeatureFlags.EnableCaching)
            _cacheStore = serviceProvider.GetRequiredService<ICacheStoreService<string, TEntity>>();
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<TSchema> CreateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        var result = await _repository.CreateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        if (_options.FeatureFlags.EnableCaching)
        {
            await InvalidateCacheAsync(new[] { entity.Id }, cancellationToken);
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<int> CreateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        int result = await _repository.CreateMultipleAsync(entities, cancellationToken);

        if (_options.FeatureFlags.EnableCaching)
        {
            await InvalidateCacheAsync(entities.Select(e => e.Id), cancellationToken);
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<TSchema> DeleteSingleAsync(string id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.DeleteSingleAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        if (_options.FeatureFlags.EnableCaching)
            await InvalidateCacheAsync(new[] { id }, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<int> DeleteMultipleAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        int result = await _repository.DeleteMultipleAsync(ids, cancellationToken);

        if (_options.FeatureFlags.EnableCaching)
            await InvalidateCacheAsync(ids, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<TSchema> GetSingleAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = _options.FeatureFlags.EnableCaching
            ? await _cacheStore?.GetAsync(id, cancellationToken)
            : null;

        if (entity is null)
        {
            entity = await _repository.ReadSingleAsync(id, cancellationToken);
            if (_options.FeatureFlags.EnableCaching)
                await _cacheStore?.SetAsync(id, entity, cancellationToken);
        }

        return _mapper.Map<TSchema>(entity);
    }

    /// <inheritdoc />
    public async Task<IQueryable<TSchema>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
    {
        var entities = _options.FeatureFlags.EnableCaching
            ? await _cacheStore?.GetMultipleAsync($"GetAllAsQueryableAsync.{typeof(TSchema).Name}", cancellationToken)
            : null;

        if (entities is null)
        {
            entities = await _repository.ReadAllAsQueryableAsync(cancellationToken);

            if (_options.FeatureFlags.EnableCaching)
                await _cacheStore?.SetMultipleAsync($"GetAllAsQueryableAsync.{typeof(TSchema).Name}", entities, cancellationToken);
        }

        return (entities as IQueryable).ProjectTo<TSchema>(_mapper.ConfigurationProvider);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default)
    {
        var entities = _options.FeatureFlags.EnableCaching
            ? await _cacheStore?.GetMultipleAsync($"GetMultipleWithLimitAsync.{limit}.{offset}", cancellationToken)
            : null;

        if (entities == null)
        {
            entities = await _repository.ReadMultipleWithLimitAsync(limit, offset, cancellationToken);

            if (_options.FeatureFlags.EnableCaching)
                await _cacheStore?.SetMultipleAsync($"GetMultipleWithLimitAsync.{limit}.{offset}", entities, cancellationToken);
        }

        return _mapper.Map<IEnumerable<TSchema>>(entities);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        var entities = Enumerable.Empty<TEntity>();

        if (_options.FeatureFlags.EnableCaching)
        {
            foreach (string id in ids)
            {
                var entity = await _cacheStore?.GetAsync(id, cancellationToken);
                if (entity == null)
                {
                    entity = await _repository.ReadSingleAsync(id, cancellationToken);
                    await _cacheStore?.SetAsync(id, entity, cancellationToken);
                }

                entities = entities.Append(entity);
            }
        }

        if (!entities.Any())
            entities = await _repository.ReadMultipleAsync(ids, cancellationToken);

        return _mapper.Map<IEnumerable<TSchema>>(entities);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var entities = _options.FeatureFlags.EnableCaching
            ? await _cacheStore?.GetMultipleAsync($"GetMultipleWithPaginationAsync.{page}.{pageSize}", cancellationToken)
            : null;

        if (entities == null)
        {
            entities = await _repository.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken);

            if (_options.FeatureFlags.EnableCaching)
                await _cacheStore?.SetMultipleAsync($"GetMultipleWithPaginationAsync.{page}.{pageSize}", entities, cancellationToken);
        }

        return _mapper.Map<IEnumerable<TSchema>>(entities);
    }

    /// <inheritdoc/>
    public async Task<TSchema> UpdateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        var result = await _repository.UpdateSingleAsync(entity, cancellationToken)
                .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        if (_options.FeatureFlags.EnableCaching)
            await InvalidateCacheAsync(new[] { entity.Id }, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<int> UpdateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        int result = await _repository.UpdateMultipleAsync(entities, cancellationToken);

        if (_options.FeatureFlags.EnableCaching)
        {
            await InvalidateCacheAsync(entities.Select(e => e.Id), cancellationToken);
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task InvalidateCacheAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
    {
        foreach (var id in ids)
            await _cacheStore?.DeleteAsync(id, cancellationToken);

        await _cacheStore?.DeleteAsync($"GetAllAsQueryableAsync.{typeof(TSchema).Name}", cancellationToken);
        await _cacheStore?.DeleteAsync($"GetMultipleWithPaginationAsync.*", cancellationToken);
        await _cacheStore?.DeleteAsync($"GetMultipleWithLimitAsync.*", cancellationToken);
    }
}
