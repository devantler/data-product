#pragma warning disable S3251
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Inputs;

namespace Devantler.DataProduct.Features.Inputs;

/// <summary>
/// Extensions for registering inputs and configuring the web application to use them.
/// </summary>
public static partial class InputsStartupExtensions
{
    /// <summary>
    /// Registers inputs to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddInputs(this IServiceCollection services, DataProductOptions options)
    {
        if (!options.Inputs.Any())
            return services;

        services.AddGeneratedServiceRegistrations(options.Inputs);

        return services;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, List<InputOptions> options);
}
#pragma warning restore S3251