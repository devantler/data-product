using Devantler.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataProduct.Generator.Tests.Unit.IncrementalGenerators.SchemaGeneratorTests;

public class GenerateTests : IncrementalGeneratorTestsBase<SchemaGenerator>
{
  [Theory]
  [MemberData(nameof(TestCases.ValidCases), MemberType = typeof(TestCases))]
  public Task GivenValidDataProductConfig_GeneratesValidCode(string subject)
  {
    //Arrange
    var additionalText = CreateDataProductConfig(
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
}
