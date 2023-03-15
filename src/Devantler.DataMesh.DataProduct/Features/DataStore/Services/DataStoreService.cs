using AutoMapper;
using AutoMapper.QueryableExtensions;
using Devantler.DataMesh.DataProduct.Features.DataStore.Entities;
using Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Services;

/// <summary>
/// Generic interface for data store services.
/// </summary>
/// <typeparam name="TSchema"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class DataStoreService<TKey, TSchema, TEntity> : IDataStoreService<TKey, TSchema>
    where TSchema : class, Schemas.ISchema<TKey>
    where TEntity : class, IEntity<TKey>
{
    readonly IRepository<TKey, TEntity> _repository;
    readonly IMapper _mapper;

    /// <summary>
    /// Constructs a new instance of <see cref="DataStoreService{TKey,TSchema, TEntity}"/>, and injects the required dependencies.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    protected DataStoreService(IRepository<TKey, TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<TSchema> CreateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        return await _repository.CreateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> CreateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        return await _repository.CreateMultipleAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TSchema> DeleteSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteSingleAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> DeleteMultipleAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        => await _repository.DeleteMultipleAsync(ids, cancellationToken);

    /// <inheritdoc/>
    public async Task<TSchema> GetSingleAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _repository.ReadSingleAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TSchema>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.ReadAllAsync(cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TSchema>>(task.Result), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IQueryable<TSchema>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.ReadAllAsQueryableAsync(cancellationToken)
            .ContinueWith(task => task.Result.ProjectTo<TSchema>(_mapper.ConfigurationProvider), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ReadMultipleWithLimitAsync(limit, offset, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TSchema>>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleAsync(IEnumerable<TKey> ids,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ReadMultipleAsync(ids, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TSchema>>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TSchema>> GetMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TSchema>>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TSchema> UpdateSingleAsync(TSchema schema, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(schema);
        return await _repository.UpdateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TSchema>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> UpdateMultipleAsync(IEnumerable<TSchema> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        return await _repository.UpdateMultipleAsync(entities, cancellationToken);
    }
}
