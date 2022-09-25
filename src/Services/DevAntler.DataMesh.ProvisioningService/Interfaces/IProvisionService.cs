namespace DevAntler.DataMesh.ProvisioningService.Interfaces;

public interface IProvisioningService
{
    public Task<Guid> Create();
    public Task Teardown(Guid id);
    public Task Enable(Guid id);
    public Task Disable(Guid id);
}
