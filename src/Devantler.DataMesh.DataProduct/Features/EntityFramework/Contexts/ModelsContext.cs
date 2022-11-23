using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.Features.EntityFramework.Contexts;

public class ModelsContext : DbContext
{
    //public required DbSet<Model> Models { get; set; }
    public ModelsContext(DbContextOptions<ModelsContext> options) : base(options)
    {
    }
}
