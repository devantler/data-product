namespace Devantler.DataMesh.Services.Dataspace.Provisioning.Targets.Local;

public class LocalProvisioningService : IProvisioningService
{
    public async Task<string[]> List()
    {
        var result = await KindCli.GetClusters();
        if (result.ExitCode != 0)
            throw new Exception("Failed to list clusters\n" + result.StandardError);
        var clusters = result.StandardOutput.Split(Environment.NewLine).ToList();
        clusters.RemoveAll(x => string.IsNullOrEmpty(x));
        return clusters.ToArray();
    }

    public async Task<string> Create()
    {
        var id = Guid.NewGuid();
        var result = await KindCli.CreateCluster(id.ToString());
        if (result.ExitCode != 0)
            throw new Exception("Failed to create cluster\n" + result.StandardError);
        return id.ToString();
    }

    public async Task Teardown(Guid id)
    {
        var result = await KindCli.DeleteCluster(id.ToString());
        if (result.ExitCode != 0)
            throw new Exception("Failed to delete cluster\n" + result.StandardError);
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
}
