using System;

namespace DevAntler.DataMesh.Provisioning;

public interface IProvisioningService
{
    public Task<Guid> Provision();
    public Task Teardown(Guid id);
    public Task Enable(Guid id);
    public Task Disable(Guid id);
}
