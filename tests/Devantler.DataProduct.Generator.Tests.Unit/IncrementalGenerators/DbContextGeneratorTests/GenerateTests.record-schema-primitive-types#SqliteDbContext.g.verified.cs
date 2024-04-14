﻿//HintName: SQLiteDbContext.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataProduct.Generator.IncrementalGenerators.DbContextGenerator'.
// Any changes made to this file will be overwritten.
using Microsoft.EntityFrameworkCore;
using Devantler.DataProduct.Features.DataStore.Entities;
namespace Devantler.DataProduct.Features.DataStore;
/// <summary>
/// A SQLite database context.
/// </summary>
public class SQLiteDbContext : DbContext
{
    /// <summary>
    /// A constructor to construct a SQLite database context.
    /// </summary>
    public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) : base(options)
    {
    }
    /// <summary>
    /// A property to access the record-schema-primitive-types table.
    /// </summary>
    public DbSet<RecordSchemaPrimitiveTypesEntity> RecordSchemaPrimitiveTypes => Set<RecordSchemaPrimitiveTypesEntity>();
    /// <summary>
    /// A method to configure the schema.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<RecordSchemaPrimitiveTypesEntity>().ToTable("RecordSchemaPrimitiveTypes");
    }
}
