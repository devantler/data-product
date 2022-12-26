namespace Devantler.DataMesh.DataProduct.Configuration;

public class OwnerOptions
{
    public const string KEY = "DataProduct:Owner";

    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
}
