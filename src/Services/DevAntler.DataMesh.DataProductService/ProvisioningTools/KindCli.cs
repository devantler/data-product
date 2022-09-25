using CliWrap;
using CliWrap.Buffered;

namespace DevAntler.DataMesh.DataProductService.ProvisioningTools;

public static class KindCli
{
    public static CommandTask<BufferedCommandResult> GetClusters()
    {
        return Cli.Wrap("kind")
            .WithArguments("get clusters")
            .ExecuteBufferedAsync();
    }

    public static CommandTask<BufferedCommandResult> CreateCluster(string name)
    {
        return Cli.Wrap("kind")
            .WithArguments($"create cluster --name {name}")
            .ExecuteBufferedAsync();
    }

    public static CommandTask<BufferedCommandResult> DeleteCluster(string name)
    {
        return Cli.Wrap("kind")
            .WithArguments($"delete cluster --name {name}")
            .ExecuteBufferedAsync();
    }
}