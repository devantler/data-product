using System.IO;
using Devantler.DataMesh.DataProduct.SourceGenerators.Extensions;
using Devantler.DataMesh.DataProduct.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Base;

public abstract class GeneratorBase
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var files = context.AdditionalTextsProvider
            .Where(additionalText => additionalText.Path.EndsWith(".data-product.yaml"))
            .Select((additionalText, cancellationToken) =>
                (Path.GetFileNameWithoutExtension(additionalText.Path),
                additionalText.GetText(cancellationToken)!.Deserialize())
            );

        var compilationAndFiles = context.CompilationProvider.Combine(files.Collect());

        context.RegisterSourceOutput(compilationAndFiles, (productionContext, sourceContext) =>
            Generate(productionContext, sourceContext.Left, sourceContext.Right[0].Item2));
    }
    public abstract void Generate(SourceProductionContext context, Compilation compilation, Configuration configuration);
}
