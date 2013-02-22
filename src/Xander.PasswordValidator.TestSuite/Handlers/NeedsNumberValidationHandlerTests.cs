using NUnit.Framework;
using Xander.PasswordValidator.Handlers;

namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class NeedsNumberValidationHandlerTests
  {
    [Test]
    public void Validate_RequiresNumberAndPasswordWithout_FailsValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.NeedsNumber = true;
      var handler = new NeedsNumberValidationHandler(settings);
      var result = handler.Validate("ThereIsNoNumberHere");
      Assert.IsFalse(result);
    }

    [Test]
    public void Validate_RequiresNumberAndPasswordWit_PassesValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.NeedsNumber = true;
      var handler = new NeedsNumberValidationHandler(settings);
      var result = handler.Validate("ThereIsANumberHere1");
      Assert.IsTrue(result);
    }
  }
}