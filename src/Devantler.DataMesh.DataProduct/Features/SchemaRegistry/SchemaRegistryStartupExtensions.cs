using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataMesh.DataProduct.Configuration.Options.SchemaRegistry.Providers;
using Devantler.DataMesh.SchemaRegistry;

namespace Devantler.DataMesh.DataProduct.Features.SchemaRegistry;

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
            SchemaRegistryType.Local => services.AddScoped<ISchemaRegistryService, LocalSchemaRegistryService>(
                service =>
                {
                    var localSchemaRegistryOptions = (LocalSchemaRegistryOptions)options.SchemaRegistry;
                    return new LocalSchemaRegistryService(
                        opt =>
                        {
                            opt.Path = "assets/" + localSchemaRegistryOptions.Path;
                        }
                    );
                }
            ),
            SchemaRegistryType.Kafka => services.AddScoped<ISchemaRegistryService, KafkaSchemaRegistryService>(
                service =>
                {
                    var kafkaSchemaRegistryOptions = (KafkaSchemaRegistryOptions)options.SchemaRegistry;
                    return new KafkaSchemaRegistryService(
                        opt =>
                        {
                            opt.Url = kafkaSchemaRegistryOptions.Url;
                        }
                    );
                }
            ),
            _ => throw new NotSupportedException($"Schema registry type {options.SchemaRegistry.Type} is not supported.")
        };
        return services;
    }
}