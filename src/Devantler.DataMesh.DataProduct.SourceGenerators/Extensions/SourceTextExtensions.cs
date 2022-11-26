using Devantler.DataMesh.DataProduct.SourceGenerators.Models;
using Microsoft.CodeAnalysis.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Devantler.DataMesh.DataProduct.SourceGenerators.Extensions;

public static class SourceTextExtensions
{
    public static Configuration Deserialize(this SourceText text)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(HyphenatedNamingConvention.Instance)
            .Build();

        return deserializer.Deserialize<Configuration>(text.ToString());
    }
}
