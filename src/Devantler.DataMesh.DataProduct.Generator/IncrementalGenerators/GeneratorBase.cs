using System.Collections.Immutable;
using System.Text;
using Devantler.DataMesh.DataProduct.Configuration.Extensions;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Generator.Extensions;
using Devantler.DataMesh.DataProduct.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

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
            var options = configuration.GetSection(DataProductOptions.Key).Get<DataProductOptions>()
                ?? throw new InvalidOperationException(
                    $"Failed to bind configuration section '{DataProductOptions.Key}' to the type '{typeof(DataProductOptions).FullName}'."
                );

            // Hack: Sets the schema registry path when the Generator run as an Analyzer. 
            // Analyzers do not support IO well (more specifically relative paths), so the path to the Avro Schemas is retrieved from AdditionalFiles instead.
            options.Services.SchemaRegistry.OverrideLocalSchemaRegistryPath(additionalFiles
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
            .Select((additionalText, _) => new AdditionalFile()
            {
                FileName = Path.GetFileName(additionalText.Path),
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(additionalText.Path),
                FilePath = additionalText.Path,
                FileDirectoryPath = Path.GetDirectoryName(additionalText.Path),
                Contents = additionalText.GetText()
            })
            .Collect();
    }

    static IConfigurationRoot BuildConfiguration(ImmutableArray<AdditionalFile> files)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var appsettings = files.FirstOrDefault(file => file.FileName.Equals("appsettings.json"))
            ?? throw new InvalidOperationException("Failed to find required 'appsettings.json' file.");
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(appsettings.Contents?.ToString()));
        configurationBuilder.AddJsonStream(textStream);
        configurationBuilder.AddJsonFile(appsettings.FilePath, optional: false);

#if DEBUG
        var appsettingsDevelopment = files.FirstOrDefault(file => file.FileName.Equals("appsettings.Development.json"));
        if (appsettingsDevelopment is not null)
            configurationBuilder.AddJsonFile(appsettingsDevelopment.FilePath, optional: true);
#elif RELEASE
        var appsettingsProduction = files.FirstOrDefault(file => file.FileName.Equals("appsettings.Production.json"));
        if (appsettingsProduction is not null)
            configurationBuilder.AddJsonFile(appsettingsProduction.FilePath, optional: true);
#endif
        var appsettingsYaml = files.FirstOrDefault(file => new string[] { ".yaml", ".yml" }.Contains(Path.GetExtension(file.FilePath)));
        if (appsettingsYaml is not null)
            configurationBuilder.AddYamlFile(appsettingsYaml.FilePath, optional: true);

        configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
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
