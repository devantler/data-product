using Devantler.DataMesh.Core.Configuration;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Devantler.DataMesh.Core.Helpers;

public static class ConfigurationDeserializer
{
    public static Config Deserialize(string configFileText)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        return deserializer.Deserialize<Config>(configFileText);
    }
}
