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
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TSchema"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class DataStoreService<TKey, TSchema, TEntity> : IDataStoreService<TKey, TSchema>
    where TSchema : class, Schemas.ISchema<TKey>
    where TEntity : class, IEntity<TKey>
{
    readonly DataProductOptions _options;
    readonly ICacheStoreService<TKey, TEntity>? _cacheStore;
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
            _cacheStore = serviceProvider.GetRequiredService<ICacheStoreService<TKey, TEntity>>();
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<TSchema> CreateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        var result = await _repository.CreateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            await _cacheStore.RemoveAsync(new[] { entity.Id }, cancellationToken);
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<int> CreateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        int result = await _repository.CreateMultipleAsync(entities, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            await _cacheStore.RemoveAsync(entities.Select(e => e.Id), cancellationToken);
        }

        return result;
    }

    /// <inheritdoc/>
    public async Task<TSchema> DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.DeleteSingleAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
            await _cacheStore.RemoveAsync(new[] { id }, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<int> DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        int result = await _repository.DeleteMultipleAsync(ids, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
            await _cacheStore.RemoveAsync(ids, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<TSchema> GetSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = _options.FeatureFlags.EnableCaching && _cacheStore is not null
            ? await _cacheStore.GetAsync(id, cancellationToken)
            : null;

        if (entity is null)
        {
            entity = await _repository.ReadSingleAsync(id, cancellationToken);
            if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
                await _cacheStore.SetAsync(id, entity, cancellationToken);
        }

        return _mapper.Map<TSchema>(entity);
    }

    /// <inheritdoc />
    public async Task<IQueryable<TSchema>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadAllAsQueryableAsync(cancellationToken);
        return entities.ProjectTo<TSchema>(_mapper.ConfigurationProvider);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default)
    {
        var entities = await _repository.ReadMultipleWithLimitAsync(limit, offset, cancellationToken);
        return _mapper.Map<IEnumerable<TSchema>>(entities);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleAsync(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        var entities = Enumerable.Empty<TEntity>();

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            foreach (var id in ids)
            {
                var entity = await _cacheStore.GetAsync(id, cancellationToken);
                if (entity == null)
                {
                    entity = await _repository.ReadSingleAsync(id, cancellationToken);
                    await _cacheStore.SetAsync(id, entity, cancellationToken);
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
        var entities = await _repository.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken);
        return _mapper.Map<IEnumerable<TSchema>>(entities);
    }

    /// <inheritdoc/>
    public async Task<TSchema> UpdateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        var result = await _repository.UpdateSingleAsync(entity, cancellationToken)
                .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
            await _cacheStore.RemoveAsync(new[] { entity.Id }, cancellationToken);

        return result;
    }

    /// <inheritdoc/>
    public async Task<int> UpdateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        int result = await _repository.UpdateMultipleAsync(entities, cancellationToken);

        if (_options.FeatureFlags.EnableCaching && _cacheStore is not null)
        {
            await _cacheStore.RemoveAsync(entities.Select(e => e.Id), cancellationToken);
        }

        return result;
    }
}