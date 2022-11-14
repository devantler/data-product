namespace Devantler.DataMesh.ProvisioningApi.Models;

public class Configuration
{
    public ClusterInfo ClusterInfo { get; set; } = new ClusterInfo();
    public string Name { get; set; }

    public Configuration(string name)
    {
        Name = name;
    }
}
