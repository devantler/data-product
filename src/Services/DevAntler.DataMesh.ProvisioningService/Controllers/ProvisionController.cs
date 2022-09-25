using DevAntler.DataMesh.ProvisioningService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAntler.DataMesh.ProvisioningService.Controllers;

[ApiController]
[Route("[controller]")]
public class ProvisioningController : ControllerBase
{
    private readonly IProvisioningService _ProvisioningService;
    private readonly ILogger<ProvisioningController> _logger;

    public ProvisioningController(IProvisioningService ProvisioningService, ILogger<ProvisioningController> logger)
    {
        _ProvisioningService = ProvisioningService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create() => Ok(_ProvisioningService.Create());

    [HttpDelete("{id}")]
    public void Teardown(Guid id) => Ok(_ProvisioningService.Teardown(id));

    [HttpPut("{id}/enable")]
    public void Enable(Guid id) => Ok(_ProvisioningService.Enable(id));

    [HttpPut("{id}/disable")]
    public void Disable(Guid id) => Ok(_ProvisioningService.Enable(id));
}
