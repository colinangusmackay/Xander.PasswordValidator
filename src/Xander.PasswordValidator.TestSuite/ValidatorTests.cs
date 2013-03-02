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

using System;
using NUnit.Framework;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.TestSuite.TestHelpers;
using Xander.PasswordValidator.TestSuite.TestHelpers.Resources;
using Xander.PasswordValidator.TestSuite.TestValidationHandlers;

namespace Xander.PasswordValidator.TestSuite
{
  [TestFixture]
  public class ValidatorTests
  {
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_SettingsIsNull_ThrowsException()
    {
      var validator = new Validator(null);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Validate_PasswordIsNull_ThrowsException()
    {
      var validator = new Validator();
      validator.Validate(null);
    }

    [Test]
    public void Validate_1CharacterPassword_FailValidation()
    {
      var settings = new PasswordValidationSettings {MinimumPasswordLength = 8};
      var validator = new Validator(settings);
      var actualResult = validator.Validate("1");

      Assert.IsFalse(actualResult);
    }

    [Test]
    public void Validate_10CharacterPassword_PassValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 8 };
      var validator = new Validator(settings);
      var actualResult = validator.Validate("1234567890");

      Assert.IsTrue(actualResult);
    }

    [Test]
    public void Validate_8CharacterPassword_PassValidaton()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 8 };
      var validator = new Validator(settings);
      var actualResult = validator.Validate("12345678");

      Assert.IsTrue(actualResult);
    }

    [Test]
    public void Validate_NeedsNumber_FailsValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsNumber = true};
      var validator = new Validator(settings);
      Assert.IsFalse(validator.Validate("ab"));
    }

    [Test]
    public void Validate_NeedsNumber_PassValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsNumber = true };
      var validator = new Validator(settings);
      Assert.IsTrue(validator.Validate("z1"));
    }

    [Test]
    public void Validate_NeedsLetter_FailsValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsLetter = true };
      var validator = new Validator(settings);
      Assert.IsFalse(validator.Validate("12"));
    }

    [Test]
    public void Validate_NeedsLetter_PassValidation()
    {
      var settings = new PasswordValidationSettings { MinimumPasswordLength = 2, NeedsLetter = true };
      var validator = new Validator(settings);
      Assert.IsTrue(validator.Validate("Z1"));
    }

    [Test]
    public void Validate_StandardWordListFemaleNames_FailValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      var validator = new Validator(settings);
      Assert.IsFalse(validator.Validate("CaTrIoNa"));
    }

    [Test]
    public void Validate_AllStandardWordLists_FailValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      settings.StandardWordLists.Add(StandardWordList.MaleNames);
      settings.StandardWordLists.Add(StandardWordList.Surnames);
      settings.StandardWordLists.Add(StandardWordList.MostCommon500Passwords);
      var validator = new Validator(settings);
      Assert.IsFalse(validator.Validate("LetMeIn"));
    }

    [Test]
    public void Validate_StandardWordListFemaleNames_PassValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      var validator = new Validator(settings);
      Assert.IsTrue(validator.Validate("CoLiN"));
    }

    [Test]
    public void Validate_AllStandardWordLists_PassValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      settings.StandardWordLists.Add(StandardWordList.MaleNames);
      settings.StandardWordLists.Add(StandardWordList.Surnames);
      settings.StandardWordLists.Add(StandardWordList.MostCommon500Passwords);
      var validator = new Validator(settings);
      Assert.IsTrue(validator.Validate("123ThisIsMyPassPhrase321"));
    }

    [Test]
    public void Validate_CustomWordLists_FailValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.CustomWordLists.Add("TestHelpers\\Files\\MyCustomWordList.txt");
      settings.CustomWordLists.Add("TestHelpers\\Files\\MyOtherCustomWordList.txt");
      var validator = new Validator(settings);
      Assert.IsFalse(validator.Validate("YetAnotherInvalidPassword"));
    }

    [Test]
    public void Validate_CustomWordLists_PassValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.CustomWordLists.Add("TestHelpers\\Files\\MyCustomWordList.txt");
      settings.CustomWordLists.Add("TestHelpers\\Files\\MyOtherCustomWordList.txt");
      var validator = new Validator(settings);
      Assert.IsTrue(validator.Validate("ThisPasswordWorks"));
    }

    [Test]
    public void Validate_AllWordsWithDoubledPassword_FailValidation()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.AllWordsConfig);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      var result = validator.Validate("Zachariah123@Zachariah123@");
      Assert.IsFalse(result);
    }

    [Test]
    public void Validate_AllWordsWithDoubledPassword_PassValidation()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.AllWordsConfig);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      var result = validator.Validate("Zachariah456@Zachariah456@");
      Assert.IsTrue(result);
    }

    [Test]
    public void Validate_CustomValidator_PassValidation()
    {
      IPasswordValidationSettings settings = new PasswordValidationSettings();
      settings.CustomValidators.Add(typeof(AlwaysPassValidationHandler));
      var validator = new Validator(settings);
      var result = validator.Validate("ThisIsMyPassword");
      Assert.IsTrue(result);
    }

    [Test]
    public void Validate_CustomValidator_FailValidation()
    {
      IPasswordValidationSettings settings = new PasswordValidationSettings();
      settings.CustomValidators.Add(typeof(AlwaysFailValidationHandler));
      var validator = new Validator(settings);
      var result = validator.Validate("ThisIsMyPassword");
      Assert.IsFalse(result);
    }

    [Test]
    public void Validate_ReversedPasswordInList_FailValidation()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.AllWordsConfig);
      PasswordValidationSection.Refresh();
      var validator = new Validator();
      var result = validator.Validate("drowssaP");
      Assert.IsFalse(result);
    }
  }
}