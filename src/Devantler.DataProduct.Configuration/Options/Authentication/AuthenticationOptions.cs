namespace Devantler.DataProduct.Configuration.Options.Authentication;

/// <summary>
/// Options for authentication.
/// </summary>
public class AuthenticationOptions
{
    /// <summary>
    /// A key to indicate the section in the configuration file that contains the authentication options.
    /// </summary>
    public const string Key = "Authentication";
    /// <summary>
    /// The type of authentication to use.
    /// </summary>
    public virtual AuthenticationType Type { get; set; } = AuthenticationType.Keycloak;
}