using NUnit.Framework;
using Xander.PasswordValidator.Handlers;

namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class NeedsSymbolValidationHandlerTests
  {
    [Test]
    public void Validate_PasswordHasSymbol_PassesValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.NeedsSymbol = true;
      var handler = new NeedsSymbolValidationHandler();
      var result = handler.Validate("ThisHas@Symbol");
      Assert.IsTrue(result);
    }

    [Test]
    public void Validate_PasswordHasSymbol_FailsValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.NeedsSymbol = true;
      var handler = new NeedsSymbolValidationHandler();
      var result = handler.Validate("ThisHasNoSymbol");
      Assert.IsFalse(result);
    }

  }
}