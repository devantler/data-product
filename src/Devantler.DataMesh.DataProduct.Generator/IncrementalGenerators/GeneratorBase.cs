using System.Collections.Immutable;
using System.Text;
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

            //TODO: Remove hack to get the path to the assembly, when omnisharp is able to set the calling assembly path correctly.
            // string assemblyPath = GetCurrentAssemblyPath(compilationAndFiles.Left);

            Generate(sourceProductionContext, compilationAndFiles.Left, configuration);
        });
    }

    static IncrementalValueProvider<ImmutableArray<(string Name, SourceText? Text)>> CollectFiles(IncrementalGeneratorInitializationContext context)
    {
        return context.AdditionalTextsProvider
            .Select((additionalFile, _) => (Name: Path.GetFileNameWithoutExtension(additionalFile.Path), Text: additionalFile.GetText()))
            .Collect();
    }

    static IConfigurationRoot BuildConfiguration(ImmutableArray<(string Name, SourceText? Text)> files)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

        foreach ((string Name, var Text) in files)
        {
            if (Name.Contains("appsettings"))
                _ = configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(Text?.ToString())));
        }
        _ = configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }

    // static string GetCurrentAssemblyPath(Compilation compilation)
    // {
    //     const string targetAssembly = "Devantler.DataMesh.DataProduct";
    //     string assemblyPath = "";
    //     if (compilation.Assembly.Name == targetAssembly)
    //         assemblyPath = compilation.Assembly.Locations.FirstOrDefault()?.SourceTree?.FilePath.Split(targetAssembly)[0] + targetAssembly + "/";

    //     return assemblyPath;
    // }

    /// <summary>
    /// Abstract method to generate code in the data product.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="compilation"></param>
    /// <param name="configuration"></param>
    public abstract void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration);
}
