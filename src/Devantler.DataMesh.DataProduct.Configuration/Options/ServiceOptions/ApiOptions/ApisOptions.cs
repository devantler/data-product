namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.ApiOptions;

/// <summary>
/// Options for the APIs
/// </summary>
public class ApisOptions
{
    /// <summary>
    /// Options for the REST API
    /// </summary>
    public RestApiOptions RestApi { get; set; } = new();

    /// <summary>
    /// Options for the GraphQL API
    /// </summary>
    public GraphQlApiOptions GraphQlApi { get; set; } = new();
}
