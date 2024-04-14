using System.Text.Json;
using Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient.Models;

namespace Devantler.DataProduct.Features.DataCatalog.Services.DataHubClient;

/// <summary>
/// A client that is responsible for interacting with DataHub's data catalog.
/// </summary>
public class Client
{
  readonly HttpClient _httpClient;

  /// <summary>
  /// Initializes a new instance of the <see cref="Client"/> class.
  /// </summary>
  public Client(HttpClient httpClient)
      => _httpClient = httpClient;

  /// <summary>
  /// Sends metadata to DataHub.
  /// </summary>
  public async Task EmitMetadata(Metadata metadata, CancellationToken cancellationToken = default)
  {
    string stringContent = JsonSerializer.Serialize(metadata.Entities, new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    });

    var request = new HttpRequestMessage(HttpMethod.Post, "openapi/entities/v1/")
    {
      Content = new StringContent(stringContent)
    };

    var response = await _httpClient.SendAsync(request, cancellationToken);

    if (!response.IsSuccessStatusCode)
      throw new HttpRequestException($"Failed to send metadata to DataHub. Status code: {(int)response.StatusCode} {response.StatusCode}.");
  }
}
