namespace DevAntler.DataMesh.Platform.Provisioning;

public interface IDataProductProvisioningService
{
    public void CreateDataProduct(Guid id);
    public void DeleteDataProduct(Guid id);
    public void DisableDataProduct(Guid id);
    public void EnableDataProduct(Guid id);
    public Task<List<string>> GetDataProducts();
}
