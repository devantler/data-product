using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.Features.DataStore.Repositories;

/// <inheritdoc cref="IProxyRepository"/>
public abstract class SQLProxyRepository : IProxyRepository
{
    readonly DbContext _context;

    /// <summary>
    /// Creates a new instance of <see cref="SQLProxyRepository"/>.
    /// </summary>
    public SQLProxyRepository(DbContext context)
        => _context = context;

    /// <inheritdoc />
    public async Task<object> ExecuteAsync(string query, object[] parameters, CancellationToken cancellationToken = default)
        => await _context.Database.ExecuteSqlRawAsync(query, parameters, cancellationToken);
}