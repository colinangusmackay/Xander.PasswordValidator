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
using System.Text.RegularExpressions;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.Handlers;
using Xander.PasswordValidator.Helpers;

namespace Xander.PasswordValidator
{
  public class Validator
  {
    private readonly IPasswordValidationSettings _settings;
    private readonly ValidationHandler _validationHandler;

    public Validator(IPasswordValidationSettings settings)
    {
      _settings = settings;
      _validationHandler = ValidationServiceLocator.GetValidationHandler(settings);
    }

    public Validator()
      : this(PasswordValidationSection.Get())
    {
    }

    public bool Validate(string password)
    {
      if (!_validationHandler.Validate(password))
        return false;

      if ((_settings.NeedsLetter) && (!password.Any(char.IsLetter)))
        return false;

      if (IsFoundInStandardWordList(password))
        return false;

      if (IsFoundInCustomWordList(password))
        return false;

      return true;
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