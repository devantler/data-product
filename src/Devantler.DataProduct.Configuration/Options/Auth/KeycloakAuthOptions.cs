namespace Devantler.DataProduct.Configuration.Options.Auth;

/// <summary>
/// Options for Keycloak.
/// </summary>
/// <remarks>
/// Check the Adaptor configs from the Keycloak admin console.
/// </remarks>
public class KeycloakAuthOptions : AuthOptions
{
  /// <inheritdoc/>
  public override AuthType Type { get; set; } = AuthType.Keycloak;

  /// <summary>
  /// The keycloak server URL.
  /// </summary>
  public string AuthServerUrl { get; set; } = string.Empty;

  /// <summary>
  /// The keycloak realm to use.
  /// </summary>
  public string Realm { get; set; } = "master";

  /// <summary>
  /// The keycloak resource/client ID to use.
  /// </summary>
  public string ClientID { get; set; } = string.Empty;

  /// <summary>
  /// The keycloak client secret to use.
  /// </summary>
  public string ClientSecret { get; set; } = string.Empty;
}
