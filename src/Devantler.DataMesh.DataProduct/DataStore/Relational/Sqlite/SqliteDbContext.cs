//TODO: Generate this class with a source generator
using Devantler.DataMesh.DataProduct.DataStore.Relational.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {
    }

    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<StudentEntity>().ToTable("Student");
        _ = modelBuilder.Entity<EnrollmentEntity>().ToTable("Enrollment");
    }
}
