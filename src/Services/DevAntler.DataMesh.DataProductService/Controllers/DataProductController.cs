using DevAntler.DataMesh.DataProductService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevAntler.DataMesh.DataProductService.Controllers;

[ApiController]
[Route("[controller]")]
public class DataProductController : ControllerBase
{
    private readonly IProvisioningService _ProvisioningService;
    private readonly ILogger<DataProductController> _logger;

    public DataProductController(IProvisioningService ProvisioningService, ILogger<DataProductController> logger)
    {
        _ProvisioningService = ProvisioningService;
        _logger = logger;
    }

    /// <summary>
    /// Lists all data products.
    /// </summary>
    [HttpGet]
    public string[] List() => _ProvisioningService.List().Result;

    /// <summary>
    /// Creates a new data product.
    /// </summary>
    [HttpPost]
    public string Create() => _ProvisioningService.Create().Result;

    /// <summary>
    /// Deletes a data product.
    /// </summary>
    [HttpDelete("{id}")]
    public void Teardown(Guid id) => Ok(_ProvisioningService.Teardown(id));

    /// <summary>
    /// Enables a data product.
    /// </summary>
    [HttpPut("{id}/enable")]
    public void Enable(Guid id) => Ok(_ProvisioningService.Enable(id));

    /// <summary>
    /// Disables a data product.
    /// </summary>
    [HttpPut("{id}/disable")]
    public void Disable(Guid id) => Ok(_ProvisioningService.Enable(id));
}
