using CliWrap;
using CliWrap.Buffered;
using DevAntler.DataMesh.ProvisioningService.Interfaces;

namespace DevAntler.DataMesh.ProvisioningService.Targets.Local;

public class LocalProvisioningService : IProvisioningService
{
    public async Task<Guid> Create()
    {
        var id = Guid.NewGuid();
        var result = await Cli.Wrap("kind")
            .WithArguments($"create cluster --name {id}")
            .ExecuteBufferedAsync();
        Console.WriteLine(result.StandardError);
        return id;
    }

    public async Task Teardown(Guid id)
    {
        var result = await Cli.Wrap("kind")
            .WithArguments($"delete cluster --name {id}")
            .ExecuteBufferedAsync();
        Console.WriteLine(result.StandardError);
    }
    public Task Disable(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    // public async Task<List<string>> GetDataProducts()
    // {
    //     var result = await Cli.Wrap("kind")
    //         .WithArguments("get clusters")
    //         .ExecuteBufferedAsync();
    //     var dataProductIds = result.StandardOutput.Split(Environment.NewLine).ToList();
    //     dataProductIds.RemoveAll(x => string.IsNullOrEmpty(x));
    //     return dataProductIds;
    // }
}
