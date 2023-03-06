using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.DbContextGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<DbContextGenerator>
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
                    "SchemaRegistry": {
                        "Type": "Local",
                        "Path": "Schemas",
                        "Schema": {
                            "Subject": "{{subject}}",
                            "Version": 1
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

    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenOtherDataStore_DoesNothing(string subject)
    {
        //Arrange
        var additionalText = CreateAppSettings(
            /*lang=json,strict*/
            $$"""
            {
                "DataProduct": {
                    "DataStore": {
                        "Type": "DocumentBased",
                        "Provider": "MongoDb",
                        "ConnectionString": "mongodb://localhost:27017"
                    },
                    "SchemaRegistry": {
                        "Type": "Local",
                        "Path": "Schemas",
                        "Schema": {
                            "Subject": "{{subject}}",
                            "Version": 1
                        }
                    }
                }
            }
            """
        );

        //Act
        var driver = RunGenerator(additionalText);

        //Assert
        return Verify(driver);
    }
}
