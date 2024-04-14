namespace Devantler.DataProduct.Configuration.Options;

/// <summary>
/// Options to configure the owner of the date product.
/// </summary>
public class OwnerOptions
{
  /// <summary>
  /// The name of the owner.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// The owner's email.
  /// </summary>
  public string Email { get; set; } = string.Empty;

  /// <summary>
  /// The organisation that the owner belongs to.
  /// </summary>
  public string Organisation { get; set; } = string.Empty;

  /// <summary>
  /// The owner's phone number..
  /// </summary>
  public string Phone { get; set; } = string.Empty;

  /// <summary>
  /// The owner's website.
  /// </summary>
  public string Website { get; set; } = string.Empty;
}
