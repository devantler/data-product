using Devantler.DataMesh.DataProduct.Features.DataStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest.DataStoreProxy;

/// <inheritdoc cref="IDataStoreProxyController"/>
[ApiController]
[Route("[controller]")]
public class DataStoreProxyController : ControllerBase, IDataStoreProxyController
{
    readonly IDataStoreProxyService _dataStoreProxyService;

    /// <summary>
    /// Creates a new instance of <see cref="DataStoreProxyController"/>.
    /// </summary>
    public DataStoreProxyController(IDataStoreProxyService dataStoreProxyService)
        => _dataStoreProxyService = dataStoreProxyService;

    /// <inheritdoc />
    [HttpPost]
    public async Task<ActionResult> ExecuteAsync([FromBody] string query, [FromQuery] object[] parameters, CancellationToken cancellationToken = default)
    {
        object result = await _dataStoreProxyService.ExecuteAsync(query, parameters, cancellationToken);
        return Ok(result);
    }
}