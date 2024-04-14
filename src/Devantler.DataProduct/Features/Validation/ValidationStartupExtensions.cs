using System.Reflection;
using FluentValidation;

namespace Devantler.DataProduct.Features.Validation;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/> to add validation.
/// </summary>
public static class ValidationStartupExtensions
{
  /// <summary>
  /// Adds validation to the DI container.
  /// </summary>
  public static IServiceCollection AddValidation(this IServiceCollection services)
  {
    _ = services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    return services;
  }
}
