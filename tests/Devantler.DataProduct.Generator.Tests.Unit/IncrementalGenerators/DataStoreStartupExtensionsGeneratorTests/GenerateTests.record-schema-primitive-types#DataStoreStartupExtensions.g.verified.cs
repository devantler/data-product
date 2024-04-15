﻿//HintName: DataStoreStartupExtensions.g.cs
// <auto-generated>
// This code was generated by: 'Devantler.DataProduct.Generator.IncrementalGenerators.DataStoreStartupExtensionsGenerator'.
// Any changes made to this file will be overwritten.
using Devantler.DataProduct.Configuration.Options.DataStore;
using Devantler.DataProduct.Features.DataStore.Services;
using Devantler.DataProduct.Features.DataStore.Repositories;
using Devantler.DataProduct.Features.DataStore.Entities;
using Devantler.DataProduct.Features.Schemas;
using Microsoft.EntityFrameworkCore;
namespace Devantler.DataProduct.Features.DataStore;
/// <summary>
/// A class that contains extension methods for service registrations and usages for a data store.
/// </summary>
public static partial class DataStoreStartupExtensions
{
    /// <summary>
    /// Adds generated service registrations for a data store.
    /// </summary>
    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, DataStoreOptions options)
    {
        _ = services.AddPooledDbContextFactory<SqliteDbContext>(dbOptions => dbOptions.UseLazyLoadingProxies().UseSqlite(options?.ConnectionString));
        _ = services.AddScoped<IRepository<Guid, RecordSchemaPrimitiveTypesEntity>, RecordSchemaPrimitiveTypesRepository>();
        _ = services.AddScoped<IDataStoreService<Guid, RecordSchemaPrimitiveTypes>, RecordSchemaPrimitiveTypesDataStoreService>();
        using var scope = services.BuildServiceProvider().CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SqliteDbContext>>();
        var context = contextFactory.CreateDbContext();
        _ = context.Database.EnsureCreated();
    }
}
