using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace Devantler.DataMesh.DataProduct.Features.Dashboard;

/// <summary>
/// Extensions for registering the dashboard feature and configuring the web application to use it.
/// </summary>
public static class DashboardStartupExtensions
{
    /// <summary>
    /// Configures the web application to use the dashboard feature.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="environment"></param>
    public static IServiceCollection AddDashboard(this IServiceCollection services, IWebHostEnvironment environment)
    {
        _ = services.AddRazorPages(opt => opt.RootDirectory = "/Features/Dashboard/UI/Pages");
        _ = services.AddServerSideBlazor(opt =>
        {
            if (environment.IsDevelopment())
                opt.DetailedErrors = true;
        });
        _ = services.AddBlazorise(options => options.Immediate = true)
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

        return services;
    }

    /// <summary>
    /// Configures the web application to use the dashboard feature.
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication UseDashboard(this WebApplication app)
    {
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