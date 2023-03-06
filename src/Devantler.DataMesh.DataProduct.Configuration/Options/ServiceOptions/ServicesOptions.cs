using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.ApiOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataSourceOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.SchemaRegistryOptions.Providers;

namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions;
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
    /// Options for the data sources.
    /// </summary>
    public IList<IDataSourceOptions> DataSources { get; set; } = new List<IDataSourceOptions>();

    /// <summary>
    /// Options for the APIs.
    /// </summary>
    public ApisOptions Apis { get; set; } = new();
}

