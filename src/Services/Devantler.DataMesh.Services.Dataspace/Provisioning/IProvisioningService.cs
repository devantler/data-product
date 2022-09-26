namespace Devantler.DataMesh.Services.Dataspace.Provisioning;

public interface IProvisioningService
{
    public Task<string> Create();
    public Task Teardown(Guid id);
    public Task Enable(Guid id);
    public Task Disable(Guid id);
    public Task<string[]> List();
}
