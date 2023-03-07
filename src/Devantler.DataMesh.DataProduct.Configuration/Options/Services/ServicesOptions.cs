using Devantler.DataMesh.DataProduct.Configuration.Options.Services.Apis;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestionSource;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore.SQL;

namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services;
/// <summary>
/// The service options.
/// </summary>
public class ServicesOptions
{
    /// <summary>
    /// Options for the schema registry used by the data product.
    /// </summary>
    public ISchemaRegistryOptions SchemaRegistry { get; set; } = new LocalSchemaRegistryOptions();

    /// <summary>
    /// Options for the data store.
    /// </summary>
    public IDataStoreOptions DataStore { get; set; } = new SqliteDataStoreOptions();

    /// <summary>
    /// Options for the data ingestion sources.
    /// </summary>
    public IList<IDataIngestionSourceOptions> DataIngestionSources { get; set; } = new List<IDataIngestionSourceOptions>();

    /// <summary>
    /// Options for the APIs.
    /// </summary>
    public ApisOptions Apis { get; set; } = new();
}
