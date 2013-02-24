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
using System.Collections.Generic;
using System.Text;

namespace Xander.PasswordValidator.Builders
{
  public class RegularExpressionDirector
  {
    private readonly string _password;
    private readonly IWordListProcessOptions _options;
    private readonly List<WordListRegExBuilder> _expressionBuilders; 
    private StringBuilder _sb;

    public static string MatchPasswordExpression(string password, IWordListProcessOptions options)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (options == null) throw new ArgumentNullException("options");

      var reb = new RegularExpressionDirector(password, options);
      reb.InitBuilders();
      var result = reb.MatchPasswordExpression();
      return result;
    }

    private RegularExpressionDirector(string password, IWordListProcessOptions options)
    {
      _password = password;
      _options = options;
      _expressionBuilders = new List<WordListRegExBuilder>();
    }
    
    private void InitBuilders()
    {
      AddPasswordBuilder();
      AddNumberSuffixBuilder();
      AddDoubledUpWordBuilder();
      AddReversedWordBuilder();
      AddCustomBuilders();
    }

    private void AddCustomBuilders()
    {
      foreach (var type in _options.CustomBuilders)
      {
        var builder = (WordListRegExBuilder) Activator.CreateInstance(type);
        _expressionBuilders.Add(builder);
      }
    }

    private void AddPasswordBuilder()
    {
      _expressionBuilders.Add(new PasswordExpressionBuilder());
    }

    private void AddReversedWordBuilder()
    {
      if (_options.CheckForReversedWord)
        _expressionBuilders.Add(new ReversedWordExpressionBuilder());
    }

    private void AddDoubledUpWordBuilder()
    {
      if (_options.CheckForDoubledUpWord)
        _expressionBuilders.Add(new DoubledUpWordExpressionBuilder());
    }

    private void AddNumberSuffixBuilder()
    {
      if (_options.CheckForNumberSuffix)
        _expressionBuilders.Add(new NumberSuffixExpressionBuilder());
    }

    private string MatchPasswordExpression()
    {
      _sb = new StringBuilder();
      AppendStartAnchor();
      AppendExpressionFragments();
      AppendEndAnchor();
      return _sb.ToString();
    }

    private void AppendExpressionFragments()
    {
      for (int i = 0; i < _expressionBuilders.Count; i++)
      {
        var expressionFragment = GetExpressionFragment(i);
        if (FragmentExists(expressionFragment))
          AppendExpressionFragment(i, expressionFragment);
      }
    }

    private static bool FragmentExists(string expressionFragment)
    {
      return !string.IsNullOrEmpty(expressionFragment);
    }

    private string GetExpressionFragment(int i)
    {
      var expressionBuilder = _expressionBuilders[i];
      string subExpression = expressionBuilder.GetRegularExpression(_password);
      return subExpression;
    }

    private void AppendExpressionFragment(int subExpressionNumber, string subExpression)
    {
      if (SubsequentExpression(subExpressionNumber))
        AppendOr();
      _sb.Append(subExpression);
    }

    private static bool SubsequentExpression(int subExpressionNumber)
    {
      return subExpressionNumber > 0;
    }

    private void AppendOr()
    {
      _sb.Append("|");
    }

    private void AppendEndAnchor()
    {
      _sb.Append("$");
    }

    private void AppendStartAnchor()
    {
      _sb.Append("^");
    }
  }
}