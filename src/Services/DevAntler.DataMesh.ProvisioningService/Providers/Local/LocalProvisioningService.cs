using CliWrap;
using CliWrap.Buffered;

namespace DevAntler.DataMesh.Platform.Provisioning.Local;

public class LocalProvisioningService : IDataProductProvisioningService
{
    public async void CreateDataProduct(Guid id)
    {
        var result = await Cli.Wrap("kind")
            .WithArguments($"create cluster --name {id}")
            .ExecuteBufferedAsync();
        Console.WriteLine(result.StandardError);
    }

    public async void DeleteDataProduct(Guid id)
    {
        var result = await Cli.Wrap("kind")
            .WithArguments($"delete cluster --name {id}")
            .ExecuteBufferedAsync();
        Console.WriteLine(result.StandardError);
    }

    public void DisableDataProduct(Guid id)
    {
        throw new NotImplementedException();
    }

    public void EnableDataProduct(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> GetDataProducts()
    {
        var result = await Cli.Wrap("kind")
            .WithArguments("get clusters")
            .ExecuteBufferedAsync();
        var dataProductIds = result.StandardOutput.Split(Environment.NewLine).ToList();
        dataProductIds.RemoveAll(x => string.IsNullOrEmpty(x));
        return dataProductIds;
    }
}
