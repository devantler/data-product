using System.Collections.Immutable;
using System.Text;
using Devantler.DataProduct.Configuration.Extensions;
using Devantler.DataProduct.Configuration.Options;
using Devantler.DataProduct.Configuration.Options.SchemaRegistry;
using Devantler.DataProduct.Generator.Extensions;
using Devantler.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataProduct.Generator.IncrementalGenerators;

/// <summary>
/// The base for generators that generate code to the data product.
/// </summary>
public abstract class GeneratorBase : IIncrementalGenerator
{
  /// <inheritdoc/>
  public void Initialize(IncrementalGeneratorInitializationContext context)
  {
    // #if DEBUG
    //         while (!System.Diagnostics.Debugger.IsAttached)
    //             Thread.Sleep(500);
    // #endif
    var collectedFiles = CollectFiles(context);

    var incrementalValueProvider = context.CompilationProvider.Combine(collectedFiles);

    context.RegisterSourceOutput(incrementalValueProvider, (sourceProductionContext, compilationAndFiles) =>
    {
      var additionalFiles = compilationAndFiles.Right;
      var configuration = BuildConfiguration(additionalFiles);
      var options = configuration.GetDataProductOptions()
              ?? throw new InvalidOperationException(
                  $"Failed to bind configuration to the type '{typeof(DataProductOptions).FullName}'."
              );

      // Hack: Sets the schema registry path when the Generator run as an Analyzer.
      // Analyzers do not support IO well (more specifically relative paths), so the path to the Avro Schemas is retrieved from AdditionalFiles instead.
      options.SchemaRegistry.OverrideLocalSchemaRegistryPath(additionalFiles
              .FirstOrDefault(x => x.FileName.EndsWith(".avsc"))?.FileDirectoryPath);

      foreach (var source in Generate(compilationAndFiles.Left, compilationAndFiles.Right, options))
      {
        sourceProductionContext.AddSource(source.Key,
                SourceText.From(source.Value.AddMetadata(GetType()), Encoding.UTF8));
      }
    });
  }

  static IncrementalValueProvider<ImmutableArray<AdditionalFile>> CollectFiles(
      IncrementalGeneratorInitializationContext context)
  {
    return context.AdditionalTextsProvider
        .Select((additionalText, _) => new AdditionalFile
        {
          FileName = Path.GetFileName(additionalText.Path),
          FilePath = additionalText.Path,
          FileDirectoryPath = Path.GetDirectoryName(additionalText.Path) ?? "",
          Contents = additionalText.GetText()
        })
        .Collect();
  }

  static IConfigurationRoot BuildConfiguration(ImmutableArray<AdditionalFile> files)
  {
    var configuration = new ConfigurationBuilder();

    var configFiles = files.Where(file => file.FileName.StartsWith("config"));

    var additionalFiles = configFiles as AdditionalFile[] ?? configFiles.ToArray();
    string configFileExtension = additionalFiles.Any(f => f.FileName.EndsWith("yaml")) ? "yaml" : "yml";

    foreach (var configFile in additionalFiles.Where(x => x.FileName.EndsWith("json")).OrderByDescending(x => x.FileName))
    {
      if (string.IsNullOrEmpty(configFile.FileDirectoryPath) && configFile.Contents != null)
      {
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(configFile.Contents.ToString()));
        _ = configuration.AddJsonStream(textStream);
      }
      else
      {
        _ = configuration.AddJsonFile(configFile.FilePath, optional: true);
      }
    }

    foreach (var configFile in additionalFiles.Where(x => x.FileName.EndsWith(configFileExtension)).OrderByDescending(x => x.FileName))
    {
      _ = configuration.AddYamlFile(configFile.FilePath, optional: true);
    }

    _ = configuration.AddEnvironmentVariables();

    return configuration.Build();
  }

  /// <summary>
  /// Generates code to the data product.
  /// </summary>
  /// <param name="compilation"></param>
  /// <param name="additionalFiles"></param>
  /// <param name="options"></param>
  public abstract Dictionary<string, string> Generate(
      Compilation compilation,
      ImmutableArray<AdditionalFile> additionalFiles,
      DataProductOptions options
  );
}
