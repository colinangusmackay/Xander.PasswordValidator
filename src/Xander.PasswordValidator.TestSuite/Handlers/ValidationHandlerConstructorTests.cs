using NUnit.Framework;
using Xander.PasswordValidator.Handlers;
using Xander.PasswordValidator.TestSuite.TestValidationHandlers;

namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class ValidationHandlerConstructorTests
  {
    [Test]
    public void ConstructHandler_SimpleValidation_TheNewValidationHandlerObject()
    {
      var settings = new PasswordValidationSettings();
      var constructor = new ValidationHandlerConstructor(typeof(AlwaysFailValidationHandler), s=>true);
      var result = constructor.ConstructHandler(settings);
      Assert.IsInstanceOf<AlwaysFailValidationHandler>(result);
    }

    [Test]
    public void ConstructHandler_SettingsHandler_TheNewValidationHandlerObject()
    {
      var settings = new PasswordValidationSettings();
      var constructor = new ValidationHandlerConstructor(typeof(ConcreteSettingsBasedValdiationHandler), s => true);
      var result = constructor.ConstructHandler(settings);
      Assert.IsInstanceOf<ConcreteSettingsBasedValdiationHandler>(result);
    }
  }
}