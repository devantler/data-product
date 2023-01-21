using System.Collections.Immutable;
using System.Text;
using Devantler.DataMesh.DataProduct.Configuration.SchemaRegistry;
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
            var configuration = BuildConfiguration(compilationAndFiles.Right);
            string localSchemaRegistryPath = GetLocalSchemaRegistryPath(compilationAndFiles.Right, configuration);
            Generate(sourceProductionContext, compilationAndFiles.Left, configuration, localSchemaRegistryPath);
        });
    }

    static IncrementalValueProvider<ImmutableArray<(string fileName, string filePath, SourceText? sourceText)>> CollectFiles(IncrementalGeneratorInitializationContext context)
    {
        return context.AdditionalTextsProvider
            .Select((additionalFile, _) => (fileName: Path.GetFileName(additionalFile.Path), filePath: additionalFile.Path, sourceText: additionalFile.GetText()))
            .Collect();
    }

    static IConfigurationRoot BuildConfiguration(ImmutableArray<(string fileName, string filePath, SourceText? sourceText)> files)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        foreach ((string fileName, _, var sourceText) in files)
        {
            if (fileName.Contains("appsettings"))
                _ = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(sourceText?.ToString())));
        }
        _ = configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }

    string GetLocalSchemaRegistryPath(ImmutableArray<(string fileName, string filePath, SourceText? sourceText)> right, IConfiguration configuration)
    {
        var schemaRegistryType = configuration
            .GetSection(SchemaRegistryOptionsBase.Key)
            .GetValue<SchemaRegistryType>(nameof(SchemaRegistryOptionsBase.Type));
        if (schemaRegistryType != SchemaRegistryType.Local)
            return string.Empty;

        var localSchemaRegistryOptions = configuration
            .GetSection(SchemaRegistryOptionsBase.Key)
            .Get<LocalSchemaRegistryOptions>();
        if (localSchemaRegistryOptions?.Path != null)
            return localSchemaRegistryOptions.Path;

        foreach ((string fileName, string filePath, _) in right)
        {
            if (fileName.EndsWith(".avsc"))
                return Path.GetDirectoryName(filePath);
        }
        return string.Empty;
    }

    /// <summary>
    /// Abstract method to generate code in the data product.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="compilation"></param>
    /// <param name="configuration"></param>
    /// <param name="localSchemaRegistryPath"></param>
    public abstract void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration, string localSchemaRegistryPath);
}
