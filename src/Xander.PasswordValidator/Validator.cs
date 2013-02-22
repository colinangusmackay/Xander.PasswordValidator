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
    private readonly IPasswordValidationSettings _settings;

    public Validator(IPasswordValidationSettings settings)
    {
      _settings = settings;
    }

    public Validator()
      : this(PasswordValidationSection.Get())
    {
    }

    public int MinPasswordLength
    {
      get { return _settings.MinimumPasswordLength; }
    }

    public bool NeedsNumber
    {
      get { return _settings.NeedsNumber; }
    }

    public bool NeedsLetter
    {
      get { return _settings.NeedsLetter; }
    }

    public IEnumerable<StandardWordList> StandardWordLists
    {
      get { return _settings.StandardWordLists; }
    }

    public IEnumerable<string> CustomWordLists
    {
      get { return _settings.CustomWordLists; }
    }

    public ValidationResult Validate(string password)
    {
      if (password.Length < _settings.MinimumPasswordLength)
        return ValidationResult.FailTooShort;

      if ((_settings.NeedsNumber) && (!password.Any(char.IsDigit)))
        return ValidationResult.FailNumberRequired;

      if ((_settings.NeedsLetter) && (!password.Any(char.IsLetter)))
        return ValidationResult.FailLetterRequired;

      if (IsFoundInStandardWordList(password))
        return ValidationResult.FailFoundInStandardList;

      if (IsFoundInCustomWordList(password))
        return ValidationResult.FailFoundInCustomList;

      return ValidationResult.Success;
    }

    private bool IsFoundInCustomWordList(string password)
    {
      if (_settings.CustomWordLists.Any())
      {
        var regex = GetRegexForPassword(password);
        foreach (string fileName in _settings.CustomWordLists)
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
      if (_settings.StandardWordLists.Any())
      {
        var regex = GetRegexForPassword(password);
        foreach (var standardWordList in _settings.StandardWordLists)
        {
          string wordList = StandardWordListRetriever.Retrieve(standardWordList);
          if (regex.IsMatch(wordList))
            return true;
        }
      }
      return false;
    }

    private Regex GetRegexForPassword(string password)
    {
      string pattern = RegularExpressionBuilder.MatchPasswordExpression(password, _settings.WordListProcessOptions);
      var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
      return regex;
    }
  }
}