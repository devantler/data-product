using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.SchemaRegistry.Client;

namespace Devantler.DataProduct.Features.SchemaRegistry;

/// <summary>
/// Extensions for registering the schema registry and configuring the web application to use it.
/// </summary>
public static class SchemaRegistryStartupExtensions
{
    /// <summary>
    /// Adds the schema registry service to the service collection.
    /// </summary>
    public static IServiceCollection AddSchemaRegistry(this IServiceCollection services, DataProductOptions options)
    {
        _ = options.SchemaRegistry.Type switch
        {
            SchemaRegistryType.Local => services.AddScoped<ISchemaRegistryClient, LocalSchemaRegistryClient>(
                _ =>
                {
                    return new LocalSchemaRegistryClient(
                        opt => opt.Path = options.SchemaRegistry.Url
                    );
                }
            ),
            SchemaRegistryType.Kafka => services.AddScoped<ISchemaRegistryClient, KafkaSchemaRegistryClient>(
                _ =>
                {
                    return new KafkaSchemaRegistryClient(
                        opt => opt.Url = options.SchemaRegistry.Url
                    );
                }
            ),
            _ => throw new NotSupportedException($"Schema registry type {options.SchemaRegistry.Type} is not supported.")
        };
        return services;
    }
}