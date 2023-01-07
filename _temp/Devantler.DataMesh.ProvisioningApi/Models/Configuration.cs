namespace Devantler.DataMesh.ProvisioningApi.DTOs;

public class Configuration
{
    public ClusterInfo ClusterInfo { get; set; } = new ClusterInfo();
    public string Name { get; set; }

    public Configuration(string name)
    {
        Name = name;
    }
}
