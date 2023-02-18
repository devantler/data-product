//TODO: Generate this class with a source generator
using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;
/// <summary>
/// A Sqlite database context.
/// </summary>
public class SqliteDbContext : DbContext
{
    /// <summary>
    /// A constructor to construct a Sqlite database context.
    /// </summary>
    /// <param name="options"></param>
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// A DbSet of Student entities.
    /// </summary>
    public DbSet<StudentEntity> Students => Set<StudentEntity>();

    /// <summary>
    /// A DbSet of Enrollment entities.
    /// </summary>
    public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();

    /// <summary>
    /// Configures the model used by the context.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<StudentEntity>().ToTable("Student");
        _ = modelBuilder.Entity<EnrollmentEntity>().ToTable("Enrollment");
    }
}
