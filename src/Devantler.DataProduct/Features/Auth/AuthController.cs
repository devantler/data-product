using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Devantler.DataProduct.Features.Auth;

/// <summary>
/// Controller for authentication and authorization.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Redirects the user to the login page, and back to the home page after login.
    /// </summary>
    /// <remarks>
    /// Challenges the OpenIdConnect middleware to authenticate the user with the configured OpenIdConnect provider.
    /// </remarks>
    [HttpGet("sign-in")]
    public IActionResult SignIn()
    {
        return User.Identity?.IsAuthenticated == false
            ? Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme)
            : Redirect("/");
    }

    /// <summary>
    /// Signs out a user.
    /// </summary>
    [HttpGet("sign-out")]
    public new IActionResult SignOut()
    {
        return base.SignOut(
            new AuthenticationProperties { RedirectUri = "/" },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}