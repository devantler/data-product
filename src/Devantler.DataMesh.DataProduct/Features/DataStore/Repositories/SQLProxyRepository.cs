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
    {
        var preparedQuery = _context.Database.GetDbConnection().CreateCommand();
        preparedQuery.CommandText = query;
        preparedQuery.Parameters.AddRange(parameters);
        var reader = await preparedQuery.ExecuteReaderAsync(cancellationToken);
        var result = new List<object>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var row = new Dictionary<string, object>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row.Add(reader.GetName(i), reader.GetValue(i));
            }
            result.Add(row);
        }
        return result;
    }
}