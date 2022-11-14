using Devantler.DataMesh.ProvisioningApi.Interfaces;
using Devantler.DataMesh.ProvisioningApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.ProvisioningApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProvisioningController : ControllerBase
{
    private readonly IDataProductProvisioningService _dataProductProvisioningService;

    public ProvisioningController(IDataProductProvisioningService dataProductProvisioningService)
    {
        _dataProductProvisioningService = dataProductProvisioningService;
    }

    /// <summary>
    /// Creates a new data product.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(string dataProductName) => Ok(await _dataProductProvisioningService.CreateAsync(new Configuration(dataProductName)));

    /// <summary>
    /// Deletes a data product.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Teardown(Guid id)
    {
        await _dataProductProvisioningService.TeardownAsync(id);
        return Ok();
    }
}
