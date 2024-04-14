namespace Devantler.DataProduct.Configuration.Options.Auth;

/// <summary>
/// Options for authentication and authorization.
/// </summary>
public class AuthOptions
{
  /// <summary>
  /// A key to indicate the section in the configuration file that contains the authentication and authorization options.
  /// </summary>
  public const string Key = "Auth";

  /// <summary>
  /// The authentication and authorization provider to use.
  /// </summary>
  public virtual AuthType Type { get; set; } = AuthType.Keycloak;
}
