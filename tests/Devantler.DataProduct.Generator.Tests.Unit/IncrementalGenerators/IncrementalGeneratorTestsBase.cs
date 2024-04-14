using System.Collections.Immutable;
using Devantler.DataProduct.Generator.IncrementalGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Devantler.DataProduct.Generator.Tests.Unit.IncrementalGenerators;

public abstract class IncrementalGeneratorTestsBase<T> where T : GeneratorBase, new()
{
  readonly CSharpCompilation _compilation;
  readonly CSharpGeneratorDriver _driver;

  protected IncrementalGeneratorTestsBase()
  {
    var references = LoadAssemblyReferences();
    _compilation = CSharpCompilation.Create(
        "Devantler.DataProduct.Generator.Tests.Unit",
        references: references
    );

    _driver = CSharpGeneratorDriver.Create(new T());
  }

  static IEnumerable<PortableExecutableReference> LoadAssemblyReferences()
  {
    return AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => !a.IsDynamic)
        .Select(a => a.Location)
        .Where(s => s.Contains("Devantler.DataProduct.dll", StringComparison.Ordinal))
        .Select(s => MetadataReference.CreateFromFile(s))
        .ToList();
  }

  protected GeneratorDriver RunGenerator(CustomAdditionalText additionalText)
  {
    ArgumentNullException.ThrowIfNull(additionalText);
    var additionalTexts = ImmutableArray.Create<AdditionalText>(additionalText);
    return _driver.AddAdditionalTexts(additionalTexts)
        .RunGenerators(_compilation);
  }

  protected static CustomAdditionalText CreateDataProductConfig(string config) =>
      new("config.json", config);
}

public class CustomAdditionalText(string path, string text) : AdditionalText
{
  public override string Path { get; } = path;

  readonly string _text = text;

  public override SourceText GetText(CancellationToken cancellationToken = default) => SourceText.From(_text);
}
