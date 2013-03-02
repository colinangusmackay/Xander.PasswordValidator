using NUnit.Framework;
namespace Xander.PasswordValidator.Web.TestSuite
{
  [TestFixture]
  public class PasswordValiationAttributeTests
  {
    [Test]
    public void IsValid_BasicSettings_IsTrue()
    {
      var attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      var result = attr.IsValid("MyPassword");
      Assert.IsTrue(result);
    }

    [Test]
    public void IsValid_LengthTooShort_IsFalse()
    {
      var attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      attr.Settings.MinimumPasswordLength = 10;
      var result = attr.IsValid("Short");
      Assert.IsFalse(result);
    }

    [Test]
    public void IsValid_NoSettings_UsesConfigFile()
    {
      var attr = new PasswordValidationAttribute();
      var result = attr.IsValid("Short");
      Assert.IsFalse(result);      
    }

    [Test]
    public void IsValid_NullPassword_FailsValidation()
    {
      // Even although the minimum length is zero, a null password always fails
      // the validation.
      var attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      attr.Settings.MinimumPasswordLength = 0;
      var result = attr.IsValid(null);
      Assert.IsFalse(result);            
    }
  }
}