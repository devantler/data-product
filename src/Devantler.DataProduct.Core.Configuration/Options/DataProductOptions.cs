using Devantler.DataProduct.Core.Configuration.Options.Apis;
using Devantler.DataProduct.Core.Configuration.Options.CacheStore;
using Devantler.DataProduct.Core.Configuration.Options.Dashboard;
using Devantler.DataProduct.Core.Configuration.Options.DataCatalog;
using Devantler.DataProduct.Core.Configuration.Options.DataIngestors;
using Devantler.DataProduct.Core.Configuration.Options.DataStore;
using Devantler.DataProduct.Core.Configuration.Options.DataStore.SQL;
using Devantler.DataProduct.Core.Configuration.Options.FeatureFlags;
using Devantler.DataProduct.Core.Configuration.Options.SchemaRegistry;
using Devantler.DataProduct.Core.Configuration.Options.SchemaRegistry.Providers;
using Devantler.DataProduct.Core.Configuration.Options.Telemetry;

namespace Devantler.DataProduct.Core.Configuration.Options;

/// <summary>
/// Options to configure a date product.
/// </summary>
public class DataProductOptions
{
    /// <summary>
    /// The name of the data product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A description of the data product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The current release of the data product.
    /// </summary>
    public string Release { get; set; } = string.Empty;

    /// <summary>
    /// The public URL of the data product.
    /// </summary>
    public string PublicUrl { get; set; } = string.Empty;

    /// <summary>
    /// Options for the license used by the data product.
    /// </summary>
    public LicenseOptions License { get; set; } = new();

    /// <summary>
    /// Options for the owner of the data product.
    /// </summary>
    public OwnerOptions Owner { get; set; } = new();

    /// <summary>
    /// Options for the features in the data product.
    /// </summary>
    public FeatureFlagsOptions FeatureFlags { get; set; } = new();

    /// <summary>
    /// Options for the dashboard.
    /// </summary>
    public DashboardOptions Dashboard { get; set; } = new();

    /// <summary>
    /// Options for the APIs.
    /// </summary>
    public ApisOptions Apis { get; set; } = new();

    /// <summary>
    /// Options for the schema registry used by the data product.
    /// </summary>
    public SchemaRegistryOptions SchemaRegistry { get; set; } = new LocalSchemaRegistryOptions();

    /// <summary>
    /// Options for the data store.
    /// </summary>
    public DataStoreOptions DataStore { get; set; } = new SqliteDataStoreOptions();

    /// <summary>
    /// Options for the cache store.
    /// </summary>
    public CacheStoreOptions CacheStore { get; set; } = new InMemoryCacheStoreOptions();

    /// <summary>
    /// Options for the telemetry exporter.
    /// </summary>
    public TelemetryOptions Telemetry { get; set; } = new ConsoleTelemetryOptions();

    /// <summary>
    /// Options for the data ingestors.
    /// </summary>
    public List<DataIngestorOptions> DataIngestors { get; set; } = new();

    /// <summary>
    /// Options for the data catalog.
    /// </summary>
    public DataCatalogOptions DataCatalog { get; set; } = new DataHubDataCatalogOptions();

    /// <summary>
    /// The environment the data product is running in.
    /// </summary>
    public string Environment { get; set; } = string.Empty;
}
