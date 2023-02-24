using AutoMapper;
using Devantler.DataMesh.DataProduct.DataStore.Entities;
using Devantler.DataMesh.DataProduct.DataStore.Repositories;

namespace Devantler.DataMesh.DataProduct.DataStore.Services;

/// <summary>
/// Generic interface for data store services.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class DataStoreService<TModel, TEntity> : IDataStoreService<TModel>
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
    public DataStoreService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(model);
        return await _repository.CreateAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<int> CreateManyAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        return await _repository.CreateManyAsync(entities, cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<TModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<int> DeleteManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => await _repository.DeleteManyAsync(ids, cancellationToken);
    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> QueryAsync(string query, CancellationToken cancellationToken = default)
    {
        return await _repository.QueryAsync(query, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<TModel> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.ReadAsync(id, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> ReadListAsync(int limit, int offset, CancellationToken cancellationToken = default)
    {
        return await _repository.ReadListAsync(limit, offset, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> ReadManyAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _repository.ReadManyAsync(ids, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<IEnumerable<TModel>> ReadPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _repository.ReadPagedAsync(page, pageSize, cancellationToken)
            .ContinueWith(task => _mapper.Map<IEnumerable<TModel>>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<TEntity>(model);
        return await _repository.UpdateAsync(entity, cancellationToken)
            .ContinueWith(task => _mapper.Map<TModel>(task.Result), cancellationToken);
    }
    /// <inheritdoc/>
    public async Task<int> UpdateManyAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(models);
        return await _repository.UpdateManyAsync(entities, cancellationToken);
    }
}
