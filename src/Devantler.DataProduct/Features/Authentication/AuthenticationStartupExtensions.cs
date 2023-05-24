using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Authentication;
using Keycloak.AuthServices.Authentication;

namespace Devantler.DataProduct.Features.Authentication;

/// <summary>
/// Extensions for registering authentication and configuring the web application to use it.
/// </summary>
public static class AuthenticationStartupExtensions
{
    /// <summary>
    /// Adds authentication to the application.
    /// </summary>
    public static IServiceCollection AddAuthentication(this IServiceCollection services, DataProductOptions options)
    {
        switch (options.Authentication.Type)
        {
            case AuthenticationType.Keycloak:
                var dataProductKeycloakOptions = (DataProduct.Configuration.Options.Authentication.KeycloakAuthenticationOptions)options.Authentication;
                var keycloakAuthenticationOptions = new Keycloak.AuthServices.Authentication.KeycloakAuthenticationOptions
                {
                    Resource = dataProductKeycloakOptions.ClientID,
                    Credentials = new Keycloak.AuthServices.Common.KeycloakClientInstallationCredentials
                    {
                        Secret = dataProductKeycloakOptions.ClientSecret
                    },
                    Realm = dataProductKeycloakOptions.Realm,
                    AuthServerUrl = dataProductKeycloakOptions.AuthServerUrl,
                    SslRequired = dataProductKeycloakOptions.SslRequired
                };
                _ = services.AddKeycloakAuthentication(keycloakAuthenticationOptions);
                return services;
            default:
                throw new NotSupportedException($"Authentication type {options.Authentication.Type} is not supported.");
        }
    }
}