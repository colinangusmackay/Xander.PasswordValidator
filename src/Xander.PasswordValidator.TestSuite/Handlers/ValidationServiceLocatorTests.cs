using NUnit.Framework;
using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class ValidationServiceLocatorTests
  {
     [Test]
     public void GetValidationHandler_SimpleSettings_ReturnsValidationHandlerWithNoSuccessor()
     {
       var settings = new PasswordValidationSettings();
       var result = ValidationServiceLocator.GetValidationHandler(settings);
       Assert.IsInstanceOf<MinimumLengthValidationHandler>(result);
       Assert.IsNull(result.Successor);
     }

    [Test]
    public void GetValidationHandler_NeedsNumber_ReturnsTwoChainedHandlers()
    {
      var settings = new PasswordValidationSettings();
      settings.NeedsNumber = true;
      var result = ValidationServiceLocator.GetValidationHandler(settings);
      Assert.IsInstanceOf<MinimumLengthValidationHandler>(result);
      Assert.IsInstanceOf<NeedsNumberValidationHandler>(result.Successor);
      Assert.IsNull(result.Successor.Successor);
    }
  }
}