using Devantler.DataMesh.DataProduct.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Devantler.DataMesh.DataProduct.Generator.Generators;

public abstract class GeneratorBase : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
// #if DEBUG
//         while (!System.Diagnostics.Debugger.IsAttached)
//             Thread.Sleep(500);
// #endif

        var files = context.AdditionalTextsProvider
            .Select((a, _) => (Path.GetFileNameWithoutExtension(a.Path), a.Path))
            .Collect();

        var incrementalValueProvider = context.CompilationProvider.Combine(files);

        context.RegisterSourceOutput(incrementalValueProvider, (sourceProductionContext, compilationAndFiles) =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            foreach (var file in compilationAndFiles.Right)
            {
                if (file.Item1.Contains("appsettings"))
                    configurationBuilder.AddJsonFile(file.Path);
            }
            configurationBuilder.AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            var dataProductOptions = configuration.GetSection(DataProductOptions.KEY).Get<DataProductOptions>();
            var assemblyName = compilationAndFiles.Left.Assembly.Name;
            var assemblyPath = compilationAndFiles.Left.Assembly.Locations.FirstOrDefault().SourceTree.FilePath.Split(assemblyName)[0] + assemblyName + "/";
            Generate(assemblyPath, sourceProductionContext, compilationAndFiles.Left, dataProductOptions);
        });
    }

    public abstract void Generate(string assemblyPath, SourceProductionContext context, Compilation left, DataProductOptions dataProductOptions);
}
