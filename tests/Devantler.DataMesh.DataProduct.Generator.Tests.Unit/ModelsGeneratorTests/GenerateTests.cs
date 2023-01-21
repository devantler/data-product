namespace Devantler.DataMesh.DataProduct.Generator.Tests.Unit.ModelsGeneratorTests;

[UsesVerify]
public class GenerateTests : ModelsGeneratorTestsBase
{
    [Theory]
    [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
    public Task GivenValidAppSettings_GeneratesValidCode(string subject)
    {
        //Act
        var additionalText = CreateAppSettingsWithLocalSchemaRegistryAndSchema(subject);

        //Assert
        return Verify(additionalText).UseMethodName(subject);
    }
}
