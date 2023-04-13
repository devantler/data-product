using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Devantler.DataProduct.Configuration.Options;

namespace Devantler.DataProduct.Features.Dashboard;

/// <summary>
/// Extensions for registering the dashboard feature and configuring the web application to use it.
/// </summary>
public static class DashboardStartupExtensions
{
    /// <summary>
    /// Configures the web application to use the dashboard feature.
    /// </summary>
    /// <param name="builder"></param>
    public static WebApplicationBuilder AddDashboard(this WebApplicationBuilder builder)
    {
        _ = builder.WebHost.UseStaticWebAssets();
        _ = builder.Services.AddRazorPages(opt => opt.RootDirectory = "/Features/Dashboard/UI/Pages");
        _ = builder.Services.AddServerSideBlazor(opt =>
        {
            if (builder.Environment.IsDevelopment())
                opt.DetailedErrors = true;
        });
        _ = builder.Services.AddBlazorise(options => options.Immediate = true)
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

        return builder;
    }

    /// <summary>
    /// Configures the web application to use the dashboard feature.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseDashboard(this WebApplication app, DataProductOptions options)
    {
        _ = app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("Content-Security-Policy", $"frame-ancestors 'self' {string.Join(" ", options.Dashboard.CSPFrameAncestors)} {string.Join(" ", options.Dashboard.EmbeddedServices.Select(x => x.Url))}");

            await next();
        });

        if (!app.Environment.IsDevelopment())
        {
            _ = app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            _ = app.UseHsts();
        }

        _ = app.UseHttpsRedirection();

        _ = app.UseStaticFiles();

        _ = app.UseRouting();

        _ = app.MapBlazorHub();
        _ = app.MapFallbackToPage("/_Host");

        return app;
    }
}