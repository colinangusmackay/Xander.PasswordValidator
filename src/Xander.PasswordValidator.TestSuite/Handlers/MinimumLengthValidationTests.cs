using NUnit.Framework;
using Xander.PasswordValidator.Handlers;

namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class MinimumLengthValidationTests
  {
    private static MinimumLengthValidationHandler GetMinimumLengthValidationHandler()
    {
      var settings = new PasswordValidationSettings();
      settings.MinimumPasswordLength = 6;
      var handler = new MinimumLengthValidationHandler(settings);
      return handler;
    }

    [Test]
    public void Validate_PasswordTooShort_ReturnsFalse()
    {
      var handler = GetMinimumLengthValidationHandler();
      var result = handler.Validate("small");
      Assert.IsFalse(result);
    }


    [Test]
    public void Validation_PasswordExactlyOnMinimumLength_ReturnsTrue()
    {
      var handler = GetMinimumLengthValidationHandler();
      var result = handler.Validate("123456");
      Assert.IsTrue(result);
    }
    [Test]
    public void Validation_PasswordOverMinimumLength_ReturnsTrue()
    {
      var handler = GetMinimumLengthValidationHandler();
      var result = handler.Validate("LargerPassword");
      Assert.IsTrue(result);
    }
  }
}