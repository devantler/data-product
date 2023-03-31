using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.IncrementalGenerators.DbContextGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<DbContextGenerator>
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
                "SchemaRegistry": {
                    "Type": "Local",
                    "Path": "schemas",
                    "Schema": {
                        "Subject": "{{subject}}",
                        "Version": 1
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
    public void GivenOtherDataStore_DoesNothing(string subject)
    {
        //Arrange
        var additionalText = CreateDataProductConfig(
            /*lang=json,strict*/
            $$"""
            {
                "DataStore": {
                    "Type": "NoSQL",
                    "Provider": "MongoDb",
                    "ConnectionString": "mongodb://localhost:27017"
                },
                "SchemaRegistry": {
                    "Type": "Local",
                    "Path": "schemas",
                    "Schema": {
                        "Subject": "{{subject}}",
                        "Version": 1
                    }
                }
            }
            """
        );

        //Act
        var driver = RunGenerator(additionalText);
        Func<Task> act = () => Verify(driver);

        //Assert
        // Check that the task contains a NotSupported exception
        _ = act.Should().ThrowAsync<NotSupportedException>();
    }
}
