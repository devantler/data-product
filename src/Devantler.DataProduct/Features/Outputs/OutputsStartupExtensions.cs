using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.Outputs;

namespace Devantler.DataProduct.Features.Outputs;

/// <summary>
/// Extensions to registers outputs and configure the web application to use them.
/// </summary>
public static partial class OutputsStartupExtensions
{
  /// <summary>
  /// Registers outputs to the DI container.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="options"></param>
  public static IServiceCollection AddOutputs(this IServiceCollection services, DataProductOptions options)
  {
    if (!options.Outputs.Any())
      return services;

    services.AddGeneratedServiceRegistrations(options.Outputs);

    return services;
  }

  static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, List<OutputOptions> options);
}

#pragma warning disable S3251
