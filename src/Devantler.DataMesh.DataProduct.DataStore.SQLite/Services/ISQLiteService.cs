using Devantler.DataMesh.DataProduct.Core.Base.Services;
using Devantler.DataMesh.DataProduct.Core.Models;

namespace Devantler.DataMesh.DataProduct.DataStore.SQLite.Services;

public interface ISQLiteService<T> : ICRUDService<T> where T : IModel
{
    
}
