using Avro;
using Devantler.DataMesh.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.ModelsGeneratorTests;

[UsesVerify]
public class GenerateTests : GeneratorTestsBase<ModelsGenerator>
{
    [Theory]
    [InlineData("", 0)]
    public Task Generate_GivenValidSchema_GeneratesModel(string subject, int version)
    {
        //Arrange
        CustomAdditionalText additionalText = AppSettingsWithLocalSchema(subject, version);

        //Act
        //Assert
        return Verify(additionalText);
    }

    [Theory]
    [InlineData("", 0)]
    public Task Generate_GivenInvalidSchema_ThrowsException(string subject, int version)
    {
        //Arrange
        CustomAdditionalText additionalText = AppSettingsWithLocalSchema(subject, version);

        //Act
        Func<Task> action = async () => await Verify(additionalText);

        //Assert
        return action.Should().ThrowAsync<CodeGenException>();
    }

    static CustomAdditionalText AppSettingsWithLocalSchema(string subject, int version) => new("appsettings.json",
        /*lang=json,strict*/
        $$"""
        {
            "DataProduct": {
                "Schema": {
                    "Subject": "{{subject}}",
                    "Version": {{version}}
                },
                "SchemaRegistry": {
                    "Type": "Local",
                    "Path": "assets/schemas"
                }
            }
        }
        """
    );
}
