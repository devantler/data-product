namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Helpers;

/// <summary>
/// A helper class for creating URNs.
/// </summary>
public static class UrnHelper
{
  /// <summary>
  /// Creates an URN for a dataset given a platform name and a dataset name.
  /// </summary>
  public static string CreateUrn(string platformName, string datasetName)
      => $"urn:li:dataset:(urn:li:dataPlatform:{platformName},{datasetName},PROD)";
}
