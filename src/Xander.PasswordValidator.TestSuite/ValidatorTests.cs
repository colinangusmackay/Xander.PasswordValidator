using NUnit.Framework;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.TestSuite.TestHelpers;
using Xander.PasswordValidator.TestSuite.TestHelpers.Resources;

namespace Xander.PasswordValidator.TestSuite
{
  [TestFixture]
  public class ValidatorTests
  {
    private Validator validator;

    [SetUp]
    public void SetUp()
    {
      validator = new Validator(8);
    }

    [Test]
    public void Validate_1CharacterPassword_FailValidation()
    {
      bool actualResult = validator.Validate("1");

      Assert.AreEqual(false, actualResult);
    }

    [Test]
    public void Validate_10CharacterPassword_PassValidation()
    {
      bool actualResult = validator.Validate("1234567890");

      Assert.AreEqual(true, actualResult);
    }

    [Test]
    public void Validate_8CharacterPassword_PassValidaton()
    {
      bool actualResult = validator.Validate("12345678");

      Assert.AreEqual(true, actualResult);
    }

    [Test]
    public void Constructor_MinPasswordLength_8Characters()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      validator = new Validator();
      Assert.AreEqual(8, validator.MinPasswordLength);
    }
  }
}