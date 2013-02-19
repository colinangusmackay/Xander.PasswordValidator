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

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.Helpers;

namespace Xander.PasswordValidator
{
  public class Validator
  {
    private readonly int _minPasswordLength;
    private readonly bool _needsNumber;
    private readonly bool _needsLetter;
    private readonly StandardWordList[] _standardWordLists;
    private readonly string[] _customWordLists;

    public Validator(IPasswordValidationSettings settings)
    {
      _minPasswordLength = settings.MinimumPasswordLength;
      _needsNumber = settings.NeedsNumber;
      _needsLetter = settings.NeedsLetter;
      _standardWordLists = settings.StandardWordLists.ToArray();
      _customWordLists = settings.CustomWordLists.ToArray();
    }

    public Validator()
      : this(PasswordValidationSection.Get())
    {
    }

    public int MinPasswordLength
    {
      get { return _minPasswordLength; }
    }

    public bool NeedsNumber
    {
      get { return _needsNumber; }
    }

    public bool NeedsLetter
    {
      get { return _needsLetter; }
    }

    public IEnumerable<StandardWordList> StandardWordLists
    {
      get { return _standardWordLists; }
    }

    public IEnumerable<string> CustomWordLists
    {
      get { return _customWordLists; }
    }

    public ValidationResult Validate(string password)
    {
      if (password.Length < _minPasswordLength)
        return ValidationResult.FailTooShort;

      if ((_needsNumber) && (!password.Any(char.IsDigit)))
        return ValidationResult.FailNumberRequired;

      if ((_needsLetter) && (!password.Any(char.IsLetter)))
        return ValidationResult.FailLetterRequired;

      if (IsFoundInStandardWordList(password))
        return ValidationResult.FailFoundInStandardList;

      if (IsFoundInCustomWordList(password))
        return ValidationResult.FailFoundInCustomList;

      return ValidationResult.Success;
    }

    private bool IsFoundInCustomWordList(string password)
    {
      if (_customWordLists.Length > 0)
      {
        var regex = GetRegexForPassword(password);
        foreach (string fileName in _customWordLists)
        {
          string wordList = CustomWordListRetriever.Retrieve(fileName);
          if (regex.IsMatch(wordList))
            return true;
        }
      }
      return false;
    }

    private bool IsFoundInStandardWordList(string password)
    {
      if (_standardWordLists.Length > 0)
      {
        var regex = GetRegexForPassword(password);
        foreach (var standardWordList in _standardWordLists)
        {
          string wordList = StandardWordListRetriever.Retrieve(standardWordList);
          if (regex.IsMatch(wordList))
            return true;
        }
      }
      return false;
    }

    private static Regex GetRegexForPassword(string password)
    {
      string pattern = RegularExpressionBuilder.MatchPasswordExpression(password);
      var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
      return regex;
    }
  }
}