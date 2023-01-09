using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational;

/// <summary>
/// Entity Framework database context used to interact with a relational database.
/// </summary>
public class RelationalDbContext : DbContext
{
    /// <summary>
    /// Constructor to construct the relational database context.
    /// </summary>
    /// <param name="options"></param>
    public RelationalDbContext(DbContextOptions<RelationalDbContext> options) : base(options)
    {
    }
}
