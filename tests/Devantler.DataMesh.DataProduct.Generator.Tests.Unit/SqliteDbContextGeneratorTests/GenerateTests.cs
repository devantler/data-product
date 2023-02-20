using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.SqliteDbContextGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<SqliteDbContextGenerator>
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
                        "Provider": "MongoDb"
                    },
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
        return Verify(driver);
    }
}
