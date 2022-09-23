using DevAntler.DataMesh.Core.Models;
using DevAntler.DataMesh.Platform.Provisioning;
using Microsoft.AspNetCore.Mvc;

namespace DevAntler.DataMesh.Platform.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DataProductController : ControllerBase
{
    private readonly ILogger<DataProductController> _logger;
    private readonly IDataProductProvisioningService _provisioningService;

    public DataProductController(ILogger<DataProductController> logger, IDataProductProvisioningService provisioningService)
    {
        _logger = logger;
        _provisioningService = provisioningService;
    }

    [HttpPost("create")]
    public IActionResult CreateDataProduct(string name, string description)
    {
        var dataProduct = new DataProduct(name, description);
        _provisioningService.CreateDataProduct(dataProduct.Id);
        _logger.LogInformation("Created data product.", dataProduct);
        return Ok(dataProduct);
    }

    [HttpPost("{id}/delete")]
    public IActionResult DeleteDataProduct(Guid id)
    {
        _provisioningService.DeleteDataProduct(id);
        _logger.LogInformation("Deleted data product.", id);
        return Ok(id);
    }

    [HttpGet]
    public IActionResult GetDataProducts()
    {
        var dataProducts = _provisioningService.GetDataProducts().Result;
        _logger.LogInformation("Retrieved data products.", dataProducts);
        return Ok(dataProducts);
    }
}
