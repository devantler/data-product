using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.GraphQlQueryGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<GraphQLQueryGenerator>
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
                    "Services": {
                        "SchemaRegistry": {
                            "Type": "Local",
                            "Path": "schemas",
                            "Schema": {
                                "Subject": "{{subject}}",
                                "Version": 1
                            }
                        }
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
