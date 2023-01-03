using System.Collections.Generic;
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
            .Select((a, _) => (Path.GetFileNameWithoutExtension(a.Path), a.Path))
            .Collect();

        var compilationProvider = context.CompilationProvider.Combine(files);
        context.RegisterSourceOutput(compilationProvider, (context, compilationAndFiles) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            Dictionary<string, string> schemas = new();
            foreach (var file in compilationAndFiles.Right)
            {
                if (file.Path.EndsWith(".avsc"))
                {
                    schemas.Add(file.Item1, file.Path);
                }
                else
                {
                    configurationBuilder.AddJsonFile(file.Path);
                }
            }
            configurationBuilder.AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            var dataProductOptions = configuration.GetSection(DataProductOptions.KEY).Get<DataProductOptions>();
            Generate(context, compilationAndFiles.Left, dataProductOptions);
        });
    }

    protected abstract void Generate(SourceProductionContext context, Compilation left, DataProductOptions dataProductOptions);
}
