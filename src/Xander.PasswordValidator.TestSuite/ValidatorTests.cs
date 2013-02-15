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
      validator = new Validator(8, false);
    }

    [Test]
    public void Validate_1CharacterPassword_FailValidation()
    {
      var actualResult = validator.Validate("1");

      Assert.AreEqual(ValidationResult.FailTooShort, actualResult);
    }

    [Test]
    public void Validate_10CharacterPassword_PassValidation()
    {
      var actualResult = validator.Validate("1234567890");

      Assert.AreEqual(ValidationResult.Success, actualResult);
    }

    [Test]
    public void Validate_8CharacterPassword_PassValidaton()
    {
      var actualResult = validator.Validate("12345678");

      Assert.AreEqual(ValidationResult.Success, actualResult);
    }

    [Test]
    public void Constructor_MinPasswordLength_8Characters()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      validator = new Validator();
      Assert.AreEqual(8, validator.MinPasswordLength);
    }

    [Test]
    public void Validate_NeedsNumber_FailsValidation()
    {
      validator = new Validator(2, true);
      Assert.AreEqual(ValidationResult.FailNumberRequired, validator.Validate("ab"));
    }

    [Test]
    public void Validate_NeedsNumber_PassValidation()
    {
      validator = new Validator(2, true);
      Assert.AreEqual(ValidationResult.Success, validator.Validate("z1"));
    }

    [Test]
    public void Constructor_NeedsNumber_True()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      validator = new Validator();
      Assert.AreEqual(true, validator.NeedsNumber);
    }
  }
}