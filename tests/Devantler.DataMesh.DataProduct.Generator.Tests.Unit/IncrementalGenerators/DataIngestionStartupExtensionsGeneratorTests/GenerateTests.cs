using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.DataIngestionStartupExtensionsGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<DataIngestionStartupExtensionsGenerator>
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidDataProductConfig_GeneratesValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateDataProductConfig(
            $$"""
            {
                "DataProduct": {
                    "FeatureFlags": {
                        "EnableDataIngestion": true
                    },
                    "Services": {
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "schemas",
                            "Schema": {
                                "Subject": "{{subject}}",
                                "Version": 1
                            }
                        },
                        "DataIngestors": [
                            {
                                "Type": "Local",
                                "FilePath": "assets/data/{{subject}}.json"
                            }
                        ]
                    }
                }
            }
            """
        );

        //Act
        var driver = RunGenerator(additionalText);

        //Assert
        return Verify(driver).UseMethodName(subject).DisableRequireUniquePrefix();
    }
}
