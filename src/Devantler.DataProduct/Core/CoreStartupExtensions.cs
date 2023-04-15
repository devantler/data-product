using Devantler.DataProduct.Core.Configuration;
using Devantler.DataProduct.Core.Configuration.Options;
using Devantler.DataProduct.Core.Mapping;
using Devantler.DataProduct.Core.Validation;

namespace Devantler.DataProduct.Core;

/// <summary>
/// Extensions for registering features and configuring the web application to use them.
/// </summary>
public static class CoreStartupExtensions
{
    /// <summary>
    /// Registers core functionality to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="args"></param>
    public static DataProductOptions AddCore(this WebApplicationBuilder builder, string[] args)
    {
        var options = builder.AddConfiguration(args);
        _ = builder.Services.AddMapping();
        _ = builder.Services.AddValidation();

        return options;
    }
}
