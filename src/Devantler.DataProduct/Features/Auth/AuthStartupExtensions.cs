using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;

namespace Devantler.DataProduct.Features.Auth;

/// <summary>
/// Extensions for registering authentication and authorization to the application.
/// </summary>
public static class AuthStartupExtensions
{
  /// <summary>
  /// Adds authentication and authorization to the DI container.
  /// </summary>
  public static IServiceCollection AddAuth(this IServiceCollection services, DataProductOptions options)
  {
    _ = services.AddHttpContextAccessor();
    _ = services.AddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();
    return options.Auth.Type switch
    {
      AuthType.Keycloak => services.AddKeycloakAuth(options),
      _ => throw new NotSupportedException($"Auth provider of type {options.Auth.Type} is not supported."),
    };
  }

  /// <summary>
  /// Configures the web application to use authentication and authorization.
  /// </summary>
  /// <param name="app"></param>
  public static WebApplication UseAuth(this WebApplication app)
  {
    _ = app.UseAuthentication();
    _ = app.UseAuthorization();
    _ = app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
      ForwardedHeaders = ForwardedHeaders.XForwardedProto
    });
    return app;
  }
}
