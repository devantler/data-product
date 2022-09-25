namespace DevAntler.DataMesh.DataProductService.Interfaces;

public interface IProvisioningService
{
    public Task<string> Create();
    public Task Teardown(Guid id);
    public Task Enable(Guid id);
    public Task Disable(Guid id);
    public Task<string[]> List();
}
