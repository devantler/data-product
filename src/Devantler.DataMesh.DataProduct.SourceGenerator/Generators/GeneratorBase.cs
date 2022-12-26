using System.IO;
using Devantler.DataMesh.DataProduct.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Generators;

public abstract class GeneratorBase : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // #if DEBUG
        //         while (!System.Diagnostics.Debugger.IsAttached)
        //             System.Threading.Thread.Sleep(500);
        // #endif

        var files = context.AdditionalTextsProvider
            .Where(a => a.Path.EndsWith("appsettings.json") || a.Path.EndsWith("appsettings.Development.json") || a.Path.EndsWith("appsettings.Production.json"))
            .Select((a, _) => (Path.GetFileNameWithoutExtension(a.Path), a.Path));

        var compilationProvider = context.CompilationProvider.Combine(files.Collect());
        context.RegisterSourceOutput(compilationProvider, (context, compilationAndFiles) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            foreach (var file in compilationAndFiles.Right)
                configurationBuilder.AddJsonFile(file.Path);
            configurationBuilder.AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            var dataProductOptions = configuration.GetSection(DataProductOptions.KEY).Get<DataProductOptions>();
            Generate(context, compilationAndFiles.Left, dataProductOptions);
        });
    }

    protected abstract void Generate(SourceProductionContext context, Compilation compilation, DataProductOptions options);
}
