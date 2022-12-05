using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational.Contexts;

public class RelationalDbContext : DbContext
{
    public RelationalDbContext(DbContextOptions<RelationalDbContext> options) : base(options)
    {
    }
}
