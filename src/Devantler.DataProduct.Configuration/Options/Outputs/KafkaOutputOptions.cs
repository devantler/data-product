namespace Devantler.DataProduct.Configuration.Options.Outputs;

/// <summary>
/// Options to configure a Kafka output for the data product.
/// </summary>
public class KafkaOutputOptions : OutputOptions
{
  /// <inheritdoc/>
  public override OutputType Type { get; set; } = OutputType.Kafka;

  /// <summary>
  /// The servers to connect to.
  /// </summary>
  public string Servers { get; set; } = string.Empty;

  /// <summary>
  /// The topic to read from.
  /// </summary>
  public string Topic { get; set; } = string.Empty;

  /// <summary>
  /// The consumer group id to use.
  /// </summary>
  public string GroupId { get; set; } = string.Empty;
}
