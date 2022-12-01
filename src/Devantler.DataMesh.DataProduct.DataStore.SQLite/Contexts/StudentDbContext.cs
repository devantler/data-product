using Microsoft.EntityFrameworkCore;
using Devantler.DataMesh.DataProduct.Core.Entities;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite.Contexts;

public class StudentDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite("Data Source=Student.db");

    public DbSet<StudentEntity> Students { get; set; } = null!;

}
