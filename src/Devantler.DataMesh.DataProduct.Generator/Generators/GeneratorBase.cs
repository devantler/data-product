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
            const string targetAssembly = "Devantler.DataMesh.DataProduct";
            var assemblyPath = "";
            if (compilationAndFiles.Left.Assembly.Name == targetAssembly)
            {
                assemblyPath = compilationAndFiles.Left.Assembly.Locations.FirstOrDefault().SourceTree.FilePath.Split(targetAssembly)[0] + targetAssembly + "/";
            }

            Generate(assemblyPath, sourceProductionContext, compilationAndFiles.Left, dataProductOptions);
        });
    }

    public abstract void Generate(string assemblyPath, SourceProductionContext context, Compilation left, DataProductOptions dataProductOptions);
}
