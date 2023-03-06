namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.ApiOptions;

public class ApisOptions
{
    public RestApiOptions RestApi { get; set; } = new();
    public GraphQlApiOptions GraphQlApi { get; set; } = new();
}
