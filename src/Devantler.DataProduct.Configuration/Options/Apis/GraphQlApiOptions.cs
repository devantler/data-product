namespace Devantler.DataProduct.Configuration.Options.Apis;

/// <summary>
/// Options for the GraphQL API.
/// </summary>
public class GraphQLApiOptions
{
  /// <summary>
  /// Enables filtering for the GraphQL endpoint.
  /// </summary>
  public bool EnableFiltering { get; set; } = true;

  /// <summary>
  /// Enables sorting for the GraphQL endpoint.
  /// </summary>
  public bool EnableSorting { get; set; } = true;

  /// <summary>
  /// Enables projections for the GraphQL endpoint.
  /// </summary>
  public bool EnableProjections { get; set; } = true;

  /// <summary>
  /// The default page size for the GraphQL endpoint.
  /// </summary>
  public int DefaultPageSize { get; set; } = 10;

  /// <summary>
  /// The maximum page size for the GraphQL endpoint.
  /// </summary>
  public int MaxPageSize { get; set; } = 100;
}
