using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.ModelsGeneratorTests;

public class ModelsGeneratorTestsBase : IncrementalGeneratorTestsBase<ModelsGenerator>
{
    protected static CustomAdditionalText CreateAppSettingsWithLocalSchemaRegistryAndSchema(string subject) => new("appsettings.json",
        $$"""
        {
            "DataProduct": {
                "Schema": {
                    "Subject": "{{subject}}",
                    "Version": 1
                },
                "SchemaRegistry": {
                    "Type": "Local",
                    "Path": "schemas"
                }
            }
        }
        """
    );

    protected static CustomAdditionalText CreateAppSettings(string appSettings) =>
        new("appsettings.json", appSettings);
}
