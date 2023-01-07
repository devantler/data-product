using System.Collections.Immutable;
using Devantler.DataMesh.DataProduct.Generator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.Generators;

public abstract class GeneratorTestsBase<T> where T : GeneratorBase, new()
{
    private readonly CSharpCompilation _compilation;
    private readonly CSharpGeneratorDriver _driver;

    protected GeneratorTestsBase()
    {
        var references = LoadAssemblyReferences();
        _compilation = CSharpCompilation.Create(
            "Devantler.DataMesh.DataProduct.Generator.Tests.Unit",
            references: references
        );

        _driver = CSharpGeneratorDriver
            .Create(new T());
    }

    public Task Verify(string[] additionalTextPaths)
    {
        var additionalTexts = CreateAdditionalTexts(additionalTextPaths);
        var driver = _driver.AddAdditionalTexts(additionalTexts.ToImmutableArray())
            .RunGenerators(_compilation);
        return Verifier.Verify(driver).UseDirectory("snapshots");
    }

    private static List<PortableExecutableReference> LoadAssemblyReferences()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic)
            .Select(a => a.Location)
            .Where(s => s.Contains("Devantler.DataMesh.DataProduct.dll"))
            .Select(s => MetadataReference.CreateFromFile(s))
            .ToList();
    }

    private static List<AdditionalText> CreateAdditionalTexts(string[] additionalTextPaths)
    {
        var additionalTexts = new List<AdditionalText>();
        foreach (string additionalTextPath in additionalTextPaths)
        {
            AdditionalText additionalText = new CustomAdditionalText(additionalTextPath);
            additionalTexts.Add(additionalText);
        }
        return additionalTexts;
    }
}

public class CustomAdditionalText : AdditionalText
{
    private readonly string _text;

    public override string Path { get; }

    public CustomAdditionalText(string path)
    {
        Path = path;
        _text = File.ReadAllText(path);
    }

    public override SourceText GetText(CancellationToken cancellationToken = new CancellationToken()) =>
        SourceText.From(_text);
}
