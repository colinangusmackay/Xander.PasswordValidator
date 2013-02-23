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

using NUnit.Framework;
using Xander.PasswordValidator.Handlers;

namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class StandardWordListValidationHandlerTests
  {
    [Test]
    public void Validate_FemaleName_FailsValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      var validator = new StandardWordListValidationHandler(settings);
      Assert.IsFalse(validator.Validate("CaThErInE"));
    }

    [Test]
    public void Validate_AllStandardWordLists_FailValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      settings.StandardWordLists.Add(StandardWordList.MaleNames);
      settings.StandardWordLists.Add(StandardWordList.Surnames);
      settings.StandardWordLists.Add(StandardWordList.MostCommon500Passwords);
      var validator = new StandardWordListValidationHandler(settings);
      Assert.IsFalse(validator.Validate("PaSsWoRd"));
    }

    [Test]
    public void Validate_StandardWordListFemaleNames_PassValidation()
    {
      var settings = new PasswordValidationSettings();
      settings.StandardWordLists.Add(StandardWordList.FemaleNames);
      var validator = new StandardWordListValidationHandler(settings);
      Assert.IsTrue(validator.Validate("OlIvEr"));
    }

  }
}