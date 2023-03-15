using Devantler.DataMesh.DataProduct.Configuration.Options.Services.Apis;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataIngestors;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore.SQL;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.SchemaRegistry.Providers;

namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services;
/// <summary>
/// The service options.
/// </summary>
public class ServicesOptions
{
    /// <summary>
    /// The key for the services options.
    /// </summary>
    public const string Key = "Services";

    /// <summary>
    /// Options for the schema registry used by the data product.
    /// </summary>
    public ISchemaRegistryOptions SchemaRegistry { get; set; } = new LocalSchemaRegistryOptions();

    /// <summary>
    /// Options for the data store.
    /// </summary>
    public IDataStoreOptions DataStore { get; set; } = new SqliteDataStoreOptions();

    /// <summary>
    /// Options for the data ingestors.
    /// </summary>
    public List<IDataIngestorOptions> DataIngestors { get; set; } = new();

    /// <summary>
    /// Options for the APIs.
    /// </summary>
    public ApisOptions Apis { get; set; } = new();
}
