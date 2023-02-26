using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;
using Devantler.DataMesh.DataProduct.Generator.Tests.Unit.GraphQlQueryGeneratorTests;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.DataStoreServiceGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<DataStoreServiceGenerator>
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidAppSettings_GeneratesValidCode(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettings(
            /*lang=json,strict*/
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
