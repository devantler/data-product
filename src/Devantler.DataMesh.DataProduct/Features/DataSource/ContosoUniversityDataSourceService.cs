using Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataSourceOptions;
using Devantler.DataMesh.DataProduct.DataSource;
using Devantler.DataMesh.DataProduct.DataStore.Services;

namespace Devantler.DataMesh.DataProduct.Models;

public sealed class ContosoUniversityDataSourceService : GenericKafkaDataSourceService<Student>
{
    public ContosoUniversityDataSourceService(IDataStoreService<Student> dataStoreService, KafkaDataSourceOptions options) : base(dataStoreService, options)
    {
    }
}
