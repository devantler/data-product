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
            var dataProductOptions = configuration.GetDataProductOptions();

            //Hack: Sets the schema registry path when the Generator run as an Analyzer. Analyzers do not support IO, so we have to get the path to the Avro Schemas through AdditionalFiles instead.
            dataProductOptions.SchemaRegistryOptions.OverrideLocalSchemaRegistryPath(additionalFiles
                .FirstOrDefault(x => x.FileName.EndsWith(".avsc"))?.FileDirectoryPath);

            var sources = Generate(compilationAndFiles.Left, compilationAndFiles.Right, dataProductOptions);

            foreach (var source in sources)
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
            .Select((additionalFile, _) => new AdditionalFile(
                Path.GetFileName(additionalFile.Path),
                additionalFile.Path,
                Path.GetDirectoryName(additionalFile.Path) ?? string.Empty,
                additionalFile.GetText())
            )
            .Collect();
    }

    static IConfigurationRoot BuildConfiguration(ImmutableArray<AdditionalFile> files)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        foreach (var file in files)
        {
            if (file.FileName.Contains("appsettings"))
            {
                _ = configurationBuilder.AddJsonStream(
                    new MemoryStream(Encoding.UTF8.GetBytes(file.Contents?.ToString() ?? string.Empty)));
            }
        }

        _ = configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }

    /// <summary>
    /// Abstract method to generate code in the data product.
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
