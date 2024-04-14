using Devantler.DataProduct.Configuration.Options;

namespace Devantler.DataProduct.Configuration.Tests.Unit;

public class DataProductOptionsTests
{
  [Fact]
  public Task DefaultPropertyValues_ShouldBeInitialized()
  {
    //Arrange
    var options = new DataProductOptions();

    //Act
    return Verify(options);
  }
}
