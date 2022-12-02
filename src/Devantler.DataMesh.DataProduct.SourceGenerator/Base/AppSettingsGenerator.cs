using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.SourceGenerator.Base;

public abstract class GeneratorBase : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // while (!System.Diagnostics.Debugger.IsAttached)
        //     System.Threading.Thread.Sleep(500);

        var files = context.AdditionalTextsProvider
            .Where(a => a.Path.EndsWith("appsettings.json"))
            .Select((a, c) => (Path.GetFileNameWithoutExtension(a.Path), a.Path.ToString()));

        var compilationProvider = context.CompilationProvider.Combine(files.Collect());
        context.RegisterSourceOutput(compilationProvider, (context, compilationAndFile) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(compilationAndFile.Right[0].Item2);
            var configuration = configurationBuilder.Build();
            Generate(context, compilationAndFile.Left, configuration);
        });
    }
    public abstract void Generate(SourceProductionContext context, Compilation compilation, IConfiguration configuration);
}
