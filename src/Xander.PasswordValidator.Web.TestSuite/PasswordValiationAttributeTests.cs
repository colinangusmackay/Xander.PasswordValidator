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

using NUnit.Framework;
using Xander.PasswordValidator.Web.Exceptions;
using Xander.PasswordValidator.Web.TestSuite.TestHelpers;

namespace Xander.PasswordValidator.Web.TestSuite
{
  [TestFixture]
  public class PasswordValiationAttributeTests
  {
    [SetUp]
    [TearDown]
    public void ResetTests()
    {
      ValidationRegistrationResetter.Reset();
    }

    [Test]
    public void IsValid_BasicSettings_IsTrue()
    {
      PasswordValidatorRegistration.Register();
      var attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      var result = attr.IsValid("MyPassword");
      Assert.IsTrue(result);
    }

    [Test]
    public void IsValid_LengthTooShort_IsFalse()
    {
      PasswordValidatorRegistration.Register();
      var attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      attr.Settings.MinimumPasswordLength = 10;
      var result = attr.IsValid("Short");
      Assert.IsFalse(result);
    }

    [Test]
    public void IsValid_NoSettings_UsesConfigFile()
    {
      PasswordValidatorRegistration.Register();
      var attr = new PasswordValidationAttribute();
      var result = attr.IsValid("Short");
      Assert.IsFalse(result);      
    }

    [Test]
    public void IsValid_NullPassword_FailsValidation()
    {
      // Even although the minimum length is zero, a null password always fails
      // the validation.
      PasswordValidatorRegistration.Register();
      var attr = new PasswordValidationAttribute();
      attr.Settings = new PasswordValidationSettings();
      attr.Settings.MinimumPasswordLength = 0;
      var result = attr.IsValid(null);
      Assert.IsFalse(result);            
    }

    [Test]
    public void IsValid_NonString_FailsValidation()
    {
      PasswordValidatorRegistration.Register();
      var attr = new PasswordValidationAttribute();
      var result = attr.IsValid(123);
      Assert.IsFalse(result);
    }

    [Test]
    [ExpectedException(typeof(PasswordValidatorRegistrationException))]
    public void IsValid_NoRegistration_ThrowsException()
    {
      var attr = new PasswordValidationAttribute();
      attr.IsValid("ThisIsMySuperSecretPassword");
    }
  }
}