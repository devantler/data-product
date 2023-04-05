using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.CRUDBulkControllerGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<CRUDBulkControllerGenerator>
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidDataProductConfig_GeneratesValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateDataProductConfig(
            /*lang=json,strict*/
            $$"""
            {
                "FeatureFlags": {
                    "EnableApis": [
                        "Rest"
                    ]
                },
                "SchemaRegistry": {
                    "Type": "Local",
                    "Path": "schemas",
                    "Schema": {
                        "Subject": "{{subject}}",
                        "Version": 1
                    }
                },
                "Apis": {
                    "Rest": {
                        "EnableBulkControllers": true
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
