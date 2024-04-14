namespace Devantler.DataProduct.Configuration.Options.Inputs;

/// <summary>
/// Options to configure a Kafka input for the data product.
/// </summary>
public class KafkaInputOptions : InputOptions
{
  /// <inheritdoc/>
  public override InputType Type { get; set; } = InputType.Kafka;

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
