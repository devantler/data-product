using Devantler.DataMesh.DataProduct.Core.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.DataProduct.Web.Features.Apis.REST;

public abstract class ControllerBase<T> : Controller, IHTTPController<T>
{
    private readonly HttpClient _httpClient;

    protected ControllerBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ActionResult> Delete(string id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync(id, cancellationToken);
        return StatusCode((int)response.StatusCode);

    }
    public async Task<ActionResult<T>> Get(string id, CancellationToken cancellationToken = default)
    {
        var model = await _httpClient.GetFromJsonAsync<T>(id, cancellationToken);
        return model != null ? Ok(model) : NotFound();
    }

    public Task<ActionResult<T>> Post(T model, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<ActionResult> Put(T model, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
