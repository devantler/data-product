using System.Security.Claims;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Auth;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Devantler.DataProduct.Features.Auth;

/// <summary>
/// Extensions to configure authentication and authorization using Keycloak.
/// </summary>
public static class KeycloakAuthStartupExtensions
{
  /// <summary>
  /// Adds authentication and authorization using Keycloak to the DI container.
  /// </summary>
  public static IServiceCollection AddKeycloakAuth(this IServiceCollection services, DataProductOptions options)
  {
    var dataProductKeycloakOptions = (KeycloakAuthOptions)options.Auth;
    _ = services.AddKeycloakWebApiAuthentication(options =>
    {
      options.Resource = dataProductKeycloakOptions.ClientID;
      options.Credentials = new KeycloakClientInstallationCredentials
      {
        Secret = dataProductKeycloakOptions.ClientSecret
      };
      options.Realm = dataProductKeycloakOptions.Realm;
      options.AuthServerUrl = dataProductKeycloakOptions.AuthServerUrl;
      options.SslRequired = "external";
    });
    _ = services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    }).AddCookie(cookie =>
    {
      cookie.Cookie.Name = "keycloak.cookie";
      cookie.Cookie.MaxAge = TimeSpan.FromMinutes(60);
      cookie.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
      cookie.SlidingExpiration = true;
    }).AddOpenIdConnect(options =>
    {
      options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      options.Authority = $"{dataProductKeycloakOptions.AuthServerUrl}realms/{dataProductKeycloakOptions.Realm}";
      options.ClientId = dataProductKeycloakOptions.ClientID;
      options.ClientSecret = dataProductKeycloakOptions.ClientSecret;
      options.MetadataAddress = $"{dataProductKeycloakOptions.AuthServerUrl}realms/{dataProductKeycloakOptions.Realm}/.well-known/openid-configuration";
      options.RequireHttpsMetadata = true;
      options.GetClaimsFromUserInfoEndpoint = true;
      options.Scope.Add("openid");
      options.Scope.Add("profile");
      options.SaveTokens = true;
      options.ResponseType = OpenIdConnectResponseType.Code;
      options.NonceCookie.SameSite = SameSiteMode.Unspecified;
      options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        NameClaimType = "name",
        RoleClaimType = ClaimTypes.Role,
        ValidateIssuer = true
      };
    });
    //_ = services.AddKeycloakAuthorization();
    _ = services.AddAuthorization();
    // services.AddAuthorization(options =>
    // {
    //     //Create policy with more than one claim
    //     options.AddPolicy("users", policy =>
    //     policy.RequireAssertion(context =>
    //     context.User.HasClaim(c =>
    //             (c.Value == "user") || (c.Value == "admin"))));
    //     //Create policy with only one claim
    //     options.AddPolicy("admins", policy =>
    //         policy.RequireClaim(ClaimTypes.Role, "admin"));
    //     //Create a policy with a claim that doesn't exist or you are unauthorized to
    //     options.AddPolicy("noaccess", policy =>
    //         policy.RequireClaim(ClaimTypes.Role, "noaccess"));
    // });
    return services;
  }
}
