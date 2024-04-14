using System.Collections.Immutable;
using Chr.Avro.Abstract;
using Devantler.Commons.CodeGen.Core.Model;
using Devantler.Commons.CodeGen.CSharp;
using Devantler.Commons.CodeGen.CSharp.Model;
using Devantler.Commons.CodeGen.Mapping.Avro;
using Devantler.Commons.StringHelpers.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.CacheStore;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// A generator for service registrations and usages for caching.
/// </summary>
[Generator]
public class CachingStartupExtensionsGenerator : GeneratorBase
{
  /// <summary>
  /// Generates service registrations and usages for caching.
  /// </summary>
  /// <param name="compilation"></param>
  /// <param name="additionalFiles"></param>
  /// <param name="options"></param>
  public override Dictionary<string, string> Generate(
      Compilation compilation,
      ImmutableArray<AdditionalFile> additionalFiles,
      DataProductOptions options)
  {
    if (!options.FeatureFlags.EnableCaching)
      return new Dictionary<string, string>();

    var schemaRegistryClient = options.SchemaRegistry.CreateSchemaRegistryClient();
    var rootSchema = schemaRegistryClient.GetSchema(options.SchemaRegistry.Schema.Subject, options.SchemaRegistry.Schema.Version);

    var codeCompilation = new CSharpCompilation();

    var @class = new CSharpClass("CachingStartupExtensions")
        .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "ICacheStoreService")))
        .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "DataProductOptions")
            .NullIfEmpty() ?? "Devantler.DataProduct.Configuration.Options")
        )
        .AddImport(new CSharpUsing(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "IEntity")))
        .SetDocBlock(new CSharpDocBlock("A class that contains extension methods for service registrations and usages for caching."))
        .SetNamespace(NamespaceResolver.ResolveForType(compilation.GlobalNamespace, "CachingStartupExtensions"))
        .SetIsStatic(true)
        .SetIsPartial(true);

    var servicesParameter = new CSharpParameter("IServiceCollection", "services");
    var optionsParameter = new CSharpParameter("DataProductOptions", "options");
    var addGeneratedServiceRegistrationsMethod = new CSharpMethod("AddGeneratedServiceRegistrations")
        .SetIsStatic(true)
        .SetIsPartial(true)
        .SetIsExtensionMethod(true)
        .SetDocBlock(new CSharpDocBlock("Adds generated service registrations for caching."))
        .SetVisibility(Visibility.Private)
        .AddParameter(servicesParameter)
        .AddParameter(optionsParameter);

    var schemas = rootSchema.Flatten().FindAll(s => s is RecordSchema).Cast<RecordSchema>();

    foreach (var schema in schemas)
    {
      _ = options.CacheStore.Type switch
      {
        CacheStoreType.InMemory => addGeneratedServiceRegistrationsMethod.AddStatement(
            $"_ = services.AddScoped<ICacheStoreService<{schema.Name}Entity>, InMemoryCacheStoreService<{schema.Name}Entity>>();"
        ),
        CacheStoreType.Redis => addGeneratedServiceRegistrationsMethod.AddStatement(
            $"_ = services.AddScoped<ICacheStoreService<{schema.Name}Entity>, RedisCacheStoreService<{schema.Name}Entity>>();"
        ),
        _ => throw new NotSupportedException($"Cache store type '{options.CacheStore.Type}' is not supported.")
      };
    }

    _ = @class.AddMethod(addGeneratedServiceRegistrationsMethod);
    _ = codeCompilation.AddType(@class);

    var codeGenerator = new CSharpCodeGenerator();
    return codeGenerator.Generate(codeCompilation);
  }
}
