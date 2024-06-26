#pragma warning disable S3251

using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.DataStore;

namespace Devantler.DataProduct.Features.DataStore;

/// <summary>
/// Extensions to registers a data store to the DI container and configure the web application to use it.
/// </summary>
public static partial class DataStoreStartupExtensions
{
  /// <summary>
  /// Registers a data store to the DI container.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="options"></param>
  /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
  public static IServiceCollection AddDataStore(this IServiceCollection services, DataProductOptions options)
  {
    services.AddGeneratedServiceRegistrations(options.DataStore);

    _ = options.DataStore.Type switch
    {
      DataStoreType.SQL => services.AddDatabaseDeveloperPageExceptionFilter(),
      DataStoreType.NoSQL => throw new NotSupportedException("Document based data stores are not supported yet."),
      DataStoreType.Graph => throw new NotSupportedException("Graph based data stores are not supported yet."),
      _ => throw new NotSupportedException($"The data store type {options.DataStore} is not supported."),
    };

    return services;
  }

  /// <summary>
  /// Configures the web application to use a data store.
  /// </summary>
  /// <param name="app"></param>
  /// <param name="options"></param>
  /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
  public static WebApplication UseDataStore(this WebApplication app, DataProductOptions options)
  {
    if (!app.Environment.IsDevelopment())
    {
      _ = app.UseExceptionHandler("/Error");
      _ = app.UseHsts();
    }
    else
    {
      _ = app.UseDeveloperExceptionPage();
    }
    switch (options.DataStore.Type)
    {
      case DataStoreType.SQL:
        break;
      case DataStoreType.NoSQL:
        throw new NotSupportedException("Document based data stores are not supported yet.");
      case DataStoreType.Graph:
        throw new NotSupportedException("Graph based data stores are not supported yet.");
      default:
        throw new NotSupportedException($"The data store type {options.DataStore.Type} is not supported.");
    }

    return app;
  }

  static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, DataStoreOptions options);
}

#pragma warning restore S3251
