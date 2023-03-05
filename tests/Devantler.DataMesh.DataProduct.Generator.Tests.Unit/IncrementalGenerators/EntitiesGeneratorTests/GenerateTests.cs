using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.EntitiesGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<EntitiesGenerator>
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidAppSettings_GeneratesValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettings(
            $$"""
            {
                "DataProduct": {
                    "Schema": {
                        "Subject": "{{subject}}",
                        "Version": 1
                    },
                    "SchemaRegistry": {
                        "Type": "Local",
                        "Path": "Schemas"
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
