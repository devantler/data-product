using Devantler.DataMesh.DataProduct.DataStore.Services;
using Devantler.DataMesh.DataProduct.Models;

namespace Devantler.DataMesh.DataProduct.Apis.GraphQL;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Student>> GetStudents([Service] IDataStoreService<Student> dataStoreService, CancellationToken cancellationToken = default)
        => await dataStoreService.GetAllAsync(cancellationToken);
}
