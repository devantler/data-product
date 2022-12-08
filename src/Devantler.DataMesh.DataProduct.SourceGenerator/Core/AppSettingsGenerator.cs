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
            Generate(context, compilationAndFiles.Left, configuration);
        });
    }

    protected abstract void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration);
}
