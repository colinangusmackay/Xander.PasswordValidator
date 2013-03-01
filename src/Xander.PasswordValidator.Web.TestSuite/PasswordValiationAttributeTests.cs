using NUnit.Framework;
namespace Xander.PasswordValidator.Web.TestSuite
{
  [TestFixture]
  public class PasswordValiationAttributeTests
  {
    [Test]
    public void IsValid_BasicSettings_IsTrue()
    {
      PasswordValidationAttribute attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      var result = attr.IsValid("MyPassword");
      Assert.IsTrue(result);
    }

    [Test]
    public void IsValid_LengthTooShort_IsFalse()
    {
      PasswordValidationAttribute attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      attr.Settings.MinimumPasswordLength = 10;
      var result = attr.IsValid("Short");
      Assert.IsFalse(result);
    }

  }
}