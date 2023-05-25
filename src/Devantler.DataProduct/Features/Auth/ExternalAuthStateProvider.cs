using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Devantler.DataProduct.Features.Auth;

/// <summary>
/// Authentication state provider for external authentication.
/// </summary>
public class ExternalAuthStateProvider : AuthenticationStateProvider
{
    readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Creates a new instance of <see cref="ExternalAuthStateProvider"/>.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public ExternalAuthStateProvider(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    /// <inheritdoc/>
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        return Task.FromResult(user?.Identity?.IsAuthenticated != true
            ? new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))
            : new AuthenticationState(user));
    }
}