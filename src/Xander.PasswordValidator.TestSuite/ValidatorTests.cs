using System.Linq;
using NUnit.Framework;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.TestSuite.TestHelpers;
using Xander.PasswordValidator.TestSuite.TestHelpers.Resources;

namespace Xander.PasswordValidator.TestSuite
{
  [TestFixture]
  public class ValidatorTests
  {
    [Test]
    public void Validate_1CharacterPassword_FailValidation()
    {
      var settings = new PasswordValidationSettings {MinimumPasswordLength = 8};
      var validator = new Validator(settings);
      var actualResult = validator.Validate("1");

      Assert.AreEqual(ValidationResult.FailTooShort, actualResult);
    }

    [Test]
    public void Validate_10CharacterPassword_PassValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 8 };
      var validator = new Validator(settings);
      var actualResult = validator.Validate("1234567890");

      Assert.AreEqual(ValidationResult.Success, actualResult);
    }

    [Test]
    public void Validate_8CharacterPassword_PassValidaton()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 8 };
      var validator = new Validator(settings);
      var actualResult = validator.Validate("12345678");

      Assert.AreEqual(ValidationResult.Success, actualResult);
    }

    [Test]
    public void Constructor_MinPasswordLength_8Characters()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      Assert.AreEqual(8, validator.MinPasswordLength);
    }

    [Test]
    public void Validate_NeedsNumber_FailsValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsNumber = true};
      var validator = new Validator(settings);
      Assert.AreEqual(ValidationResult.FailNumberRequired, validator.Validate("ab"));
    }

    [Test]
    public void Validate_NeedsNumber_PassValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsNumber = true };
      var validator = new Validator(settings);
      Assert.AreEqual(ValidationResult.Success, validator.Validate("z1"));
    }

    [Test]
    public void Constructor_NeedsNumber_True()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      Assert.AreEqual(true, validator.NeedsNumber);
    }

    [Test]
    public void Validate_NeedsLetter_FailsValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsLetter = true };
      var validator = new Validator(settings);
      Assert.AreEqual(ValidationResult.FailLetterRequired, validator.Validate("12"));
    }

    [Test]
    public void Validate_NeedsLetter_PassValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsLetter = true };
      var validator = new Validator(settings);
      Assert.AreEqual(ValidationResult.Success, validator.Validate("Z1"));
    }

    [Test]
    public void Constructor_NeedsLetter_True()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      Assert.AreEqual(true, validator.NeedsLetter);
    }

    [Test]
    public void Constructor_StandardWordLists_Empty()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      Assert.IsFalse(validator.StandardWordLists.Any());
    }

  }
}