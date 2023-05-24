namespace Devantler.DataProduct.Configuration.Options.Authentication;

/// <summary>
/// Options for Keycloak authentication.
/// </summary>
/// <remarks>
/// Check the Adaptor configs from the Keycloak admin console.
/// </remarks>
public class KeycloakAuthenticationOptions : AuthenticationOptions
{
    /// <inheritdoc/>
    public override AuthenticationType Type { get; set; } = AuthenticationType.Keycloak;

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

    /// <summary>
    /// Whether to use SSL for the connection to the keycloak server.
    /// </summary>
    /// <remarks>
    /// Can be "external" or "none".
    /// </remarks>
    public string SslRequired { get; set; } = "external";
}