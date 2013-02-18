#region Copyright notice
/******************************************************************************
 * Copyright (C) 2013 Colin Angus Mackay
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 * 
 ******************************************************************************
 *
 * For more information visit: 
 * https://github.com/colinangusmackay/Xander.PasswordValidator
 * 
 *****************************************************************************/
#endregion

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