using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataSourceOptions;
using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Devantler.DataMesh.DataProduct.Models;

namespace Devantler.DataMesh.DataProduct.Features.DataSource;

/// <summary>
/// Data Source for Contoso University data
/// </summary>
public sealed class ContosoUniversityDataSourceService : GenericKafkaDataSourceService<Student>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContosoUniversityDataSourceService"/> class.
    /// </summary>
    public ContosoUniversityDataSourceService(IDataStoreService<Student> dataStoreService, KafkaDataSourceOptions options) : base(dataStoreService, options)
    {
    }
}
