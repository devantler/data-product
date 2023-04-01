using System.Collections.Immutable;
using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators;

public abstract class IncrementalGeneratorTestsBase<T> where T : GeneratorBase, new()
{
    readonly CSharpCompilation _compilation;
    readonly CSharpGeneratorDriver _driver;

    protected IncrementalGeneratorTestsBase()
    {
        var references = LoadAssemblyReferences();
        _compilation = CSharpCompilation.Create(
            "Devantler.DataMesh.DataProduct.Generator.Tests.Unit",
            references: references
        );

        _driver = CSharpGeneratorDriver.Create(new T());
    }

    static IEnumerable<PortableExecutableReference> LoadAssemblyReferences()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic)
            .Select(a => a.Location)
            .Where(s => s.Contains("Devantler.DataMesh.DataProduct.dll", StringComparison.Ordinal))
            .Select(s => MetadataReference.CreateFromFile(s))
            .ToList();
    }

    protected GeneratorDriver RunGenerator(CustomAdditionalText additionalText)
    {
        if (additionalText is null)
            throw new ArgumentNullException(nameof(additionalText));
        var additionalTexts = ImmutableArray.Create<AdditionalText>(additionalText);
        return _driver.AddAdditionalTexts(additionalTexts)
            .RunGenerators(_compilation);
    }

    protected static CustomAdditionalText CreateDataProductConfig(string config) =>
        new("config.json", config);
}

public class CustomAdditionalText : AdditionalText
{
    public override string Path { get; } = string.Empty;

    readonly string _text;

    public CustomAdditionalText(string path, string text)
    {
        Path = path;
        _text = text;
    }

    public override SourceText GetText(CancellationToken cancellationToken = default) => SourceText.From(_text);
}
