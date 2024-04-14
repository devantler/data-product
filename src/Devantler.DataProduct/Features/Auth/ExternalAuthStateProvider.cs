using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Devantler.DataProduct.Features.Auth;

/// <summary>
/// Authentication state provider for external authentication.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="ExternalAuthStateProvider"/>.
/// </remarks>
/// <param name="httpContextAccessor"></param>
public class ExternalAuthStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
{
  readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

  /// <inheritdoc/>
  public override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    var user = _httpContextAccessor.HttpContext?.User;

    return Task.FromResult(user?.Identity?.IsAuthenticated != true
        ? new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))
        : new AuthenticationState(user));
  }
}
