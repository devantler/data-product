namespace Devantler.DataProduct.Configuration.Options.Apis;

/// <summary>
/// Options for the APIs
/// </summary>
public class ApisOptions
{
  /// <summary>
  /// Options for the REST API
  /// </summary>
  public RestApiOptions Rest { get; set; } = new();

  /// <summary>
  /// Options for the GraphQL API
  /// </summary>
  public GraphQLApiOptions GraphQL { get; set; } = new();
}
