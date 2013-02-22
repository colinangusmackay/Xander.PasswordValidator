using NUnit.Framework;
using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class ValidationServiceLocatorTests
  {
     [Test]
     public void GetValidationHandler_SimpleSettings_ReturnsValidationHandler()
     {
       var settings = new PasswordValidationSettings();
       var result = ValidationServiceLocator.GetValidationHandler(settings);
       Assert.IsInstanceOf<MinimumLengthValidationHandler>(result);
     }
  }
}