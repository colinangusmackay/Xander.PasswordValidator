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
using System.Text.RegularExpressions;
using NUnit.Framework;
using Xander.PasswordValidator.Helpers;

namespace Xander.PasswordValidator.TestSuite.Helpers
{
  [TestFixture]
  public class RegularExpressionBuilderTests
  {
    private void AssertRegularExpressionIsValid(string test, string pattern)
    {
      var regex = new Regex(pattern);
      Assert.IsTrue(regex.IsMatch(test));
    }

    [Test]
    public void BuildPasswordExpression_LetterOnlyPassword_ValidRegularExpression()
    {
      string password = "SimplePassword";
      var options = new WordListProcessOptionsSettings();
      string result = RegularExpressionBuilder.MatchPasswordExpression(password, options);
      Assert.AreEqual("^SimplePassword$", result);
      AssertRegularExpressionIsValid(password, result);
    }

    [Test]
    public void BuildPasswordExpression_AlphaNumericPassword_ValidRegularExpression()
    {
      string password = "S1mpl3P455w0rd";
      var options = new WordListProcessOptionsSettings();
      string result = RegularExpressionBuilder.MatchPasswordExpression(password, options);
      Assert.AreEqual("^S1mpl3P455w0rd$", result);
      AssertRegularExpressionIsValid(password, result);
    }

    [Test]
    public void BuildPasswordExpression_AllTheEscapes_ValidRegularExpression()
    {
      string password = @"dot=. dollar=$ Circumflex=^ open-brace={ open-square=[ open-bracket=( vertical-line=| close-bracket=) asterisk=* plus=+ question=? backslash=\";
      string expected = @"^dot=\. dollar=\$ Circumflex=\^ open-brace=\{ open-square=\[ open-bracket=\( vertical-line=\| close-bracket=\) asterisk=\* plus=\+ question=\? backslash=\\$";
      var options = new WordListProcessOptionsSettings();
      string result = RegularExpressionBuilder.MatchPasswordExpression(password, options);
      Assert.AreEqual(expected, result);
      AssertRegularExpressionIsValid(password, result);
    }

    [Test]
    public void BuildPasswordExpression_Symbols_ValidRegularExpression()
    {
      string password = "¬!\"£$%^&*()_+`-={}[]:@~;'#<>?,./|\\";
      string expected = "^¬!\"£\\$%\\^&\\*\\(\\)_\\+`-=\\{}\\[]:@~;'#<>\\?,\\./\\|\\\\$";
      var options = new WordListProcessOptionsSettings();
      string result = RegularExpressionBuilder.MatchPasswordExpression(password, options);
      Assert.AreEqual(expected, result);
      AssertRegularExpressionIsValid(password, result);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void BuildPasswordExpression_NullPassword_ThrowsException()
    {
      var options = new WordListProcessOptionsSettings();
      RegularExpressionBuilder.MatchPasswordExpression(null, options);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void BuildPasswordExpression_NullOptions_ThrowsException()
    {
      RegularExpressionBuilder.MatchPasswordExpression("this is a password", null);
    }

    [Test]
    public void BuildPasswordExpression_NumberSuffixOptionOn_ValidRegularExpression()
    {
      var options = new WordListProcessOptionsSettings();
      options.CheckForNumberSuffix = true;
      var result = RegularExpressionBuilder.MatchPasswordExpression("MyPassword", options);
      Assert.AreEqual("^MyPassword|MyPassword[0-9]$", result);
    }

    [Test]
    public void BuildPasswordExpression_DoubledUpWordOptionOnWithDoubledUpWord_ValidRegularExpression()
    {
      var options = new WordListProcessOptionsSettings();
      options.CheckForDoubledUpWord = true;
      var result = RegularExpressionBuilder.MatchPasswordExpression("PasswordPassword", options);
      Assert.AreEqual("^PasswordPassword|Password$", result);
    }

    [Test]
    public void BuildPasswordExpression_DoubledUpWordOptionOnWithoutDoubledUpWord_ValidRegularExpression()
    {
      var options = new WordListProcessOptionsSettings();
      options.CheckForDoubledUpWord = true;
      var result = RegularExpressionBuilder.MatchPasswordExpression("ThePassword", options);
      Assert.AreEqual("^ThePassword$", result);
    }
    
    [Test]
    public void BuildPasswordExpression_AllOptionsOn_ValidRegularExpression()
    {
      var options = new WordListProcessOptionsSettings();
      options.CheckForNumberSuffix = true;
      options.CheckForDoubledUpWord = true;
      var result = RegularExpressionBuilder.MatchPasswordExpression("PasswordPassword", options);
      string expected = "^PasswordPassword|PasswordPassword[0-9]|Password$";
      Assert.AreEqual(expected, result);
    }
  }
}