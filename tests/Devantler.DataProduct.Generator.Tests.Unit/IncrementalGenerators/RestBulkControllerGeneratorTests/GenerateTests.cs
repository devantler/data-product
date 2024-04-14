using Devantler.DataProduct.Generator.IncrementalGenerators;

namespace Devantler.DataProduct.Generator.Tests.Unit.IncrementalGenerators.RestBulkControllerGeneratorTests;

[UsesVerify]
public class GenerateTests : IncrementalGeneratorTestsBase<RestBulkControllerGenerator>
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
                        "EnableBulkController": true
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
