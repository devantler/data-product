using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Core;

public abstract class AppSettingsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // while (!System.Diagnostics.Debugger.IsAttached)
        //     System.Threading.Thread.Sleep(500);

        var files = context.AdditionalTextsProvider
            .Where(a => a.Path.EndsWith("appsettings.json"))
            .Select((a, _) => (Path.GetFileNameWithoutExtension(a.Path), a.Path));

        var compilationProvider = context.CompilationProvider.Combine(files.Collect());
        context.RegisterSourceOutput(compilationProvider, (context, compilationAndFiles) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(compilationAndFiles.Right[0].Path);
            var configuration = configurationBuilder.Build();
            Generate(context, compilationAndFiles.Left, configuration);
        });
    }

    protected abstract void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration);
}
