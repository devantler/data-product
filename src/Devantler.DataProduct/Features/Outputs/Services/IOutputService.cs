using Devantler.DataProduct.Features.Schemas;

namespace Devantler.DataProduct.Features.Outputs.Services;

/// <summary>
/// An output that sends data to an external target.
/// </summary>
public interface IOutputService<TKey, TSchema> where TSchema : class, ISchema<TKey>
{
  /// <summary>
  /// Sends data to an external target.
  /// </summary>
  /// <param name="schema"></param>
  /// <param name="method"></param>
  /// <param name="cancellationToken"></param>
  public Task SendAsync(TSchema schema, string method, CancellationToken cancellationToken = default);

  /// <summary>
  /// Sends data to an external target.
  /// </summary>
  /// <param name="schemas"></param>
  /// <param name="method"></param>
  /// <param name="cancellationToken"></param>
  public Task SendAsync(IEnumerable<TSchema> schemas, string method, CancellationToken cancellationToken = default);
}
