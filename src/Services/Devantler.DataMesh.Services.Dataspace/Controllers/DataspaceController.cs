using Devantler.DataMesh.Services.Dataspace.Provisioning;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataMesh.Services.Dataspace.Controllers;

[ApiController]
[Route("[controller]")]
public class DataspaceController : ControllerBase
{
    private readonly IProvisioningService _ProvisioningService;
    private readonly ILogger<DataspaceController> _logger;

    public DataspaceController(IProvisioningService ProvisioningService, ILogger<DataspaceController> logger)
    {
        _ProvisioningService = ProvisioningService;
        _logger = logger;
    }

    /// <summary>
    /// Lists all dataspaces.
    /// </summary>
    [HttpGet]
    public string[] List() => _ProvisioningService.List().Result;

    /// <summary>
    /// Creates a new dataspace.
    /// </summary>
    [HttpPost]
    public string Create() => _ProvisioningService.Create().Result;

    /// <summary>
    /// Deletes a dataspace.
    /// </summary>
    [HttpDelete("{id}")]
    public void Teardown(Guid id) => Ok(_ProvisioningService.Teardown(id));

    /// <summary>
    /// Enables a dataspace.
    /// </summary>
    [HttpPut("{id}/enable")]
    public void Enable(Guid id) => Ok(_ProvisioningService.Enable(id));

    /// <summary>
    /// Disables a dataspace.
    /// </summary>
    [HttpPut("{id}/disable")]
    public void Disable(Guid id) => Ok(_ProvisioningService.Enable(id));
}
