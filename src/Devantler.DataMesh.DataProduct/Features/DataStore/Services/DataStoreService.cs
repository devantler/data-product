using AutoMapper;
using AutoMapper.QueryableExtensions;
using Devantler.DataMesh.DataProduct.DataStore.Entities;
using Devantler.DataMesh.DataProduct.DataStore.Repositories;

namespace Devantler.DataMesh.DataProduct.DataStore.Services;

/// <summary>
/// Generic interface for data store services.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class DataStoreService<TModel, TEntity> : IDataStoreService<TModel>
    where TModel : class
    where TEntity : class, IEntity
{
    readonly IRepository<TEntity> _repository;
    readonly IMapper _mapper;

    /// <summary>
    /// Constructs a new instance of <see cref="DataStoreService{TModel, TEntity}"/>, and injects the required dependencies.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    protected DataStoreService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<TModel> CreateSingleAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(model);
        return await _repository.CreateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> CreateMultipleAsync(IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        return await _repository.CreateMultipleAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TModel> DeleteSingleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteSingleAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => await _repository.DeleteMultipleAsync(ids, cancellationToken);

    /// <inheritdoc/>
    public async Task<TModel> GetSingleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ReadSingleAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.ReadAllAsync(cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IQueryable<TModel>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.ReadAllAsQueryableAsync(cancellationToken)
            .ContinueWith(task => task.Result.ProjectTo<TModel>(_mapper.ConfigurationProvider), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> GetMultipleWithLimitAsync(int limit, int offset,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ReadMultipleWithLimitAsync(limit, offset, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> GetMultipleAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ReadMultipleAsync(ids, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> GetMultipleWithPaginationAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ReadMultipleWithPaginationAsync(page, pageSize, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TModel> UpdateSingleAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(model);
        return await _repository.UpdateSingleAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<int> UpdateMultipleAsync(IEnumerable<TModel> models,
        CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        return await _repository.UpdateMultipleAsync(entities, cancellationToken);
    }
}
