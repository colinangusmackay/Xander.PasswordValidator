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
using System.Linq;
using System.Text;
namespace Xander.PasswordValidator.Helpers
{
  public class RegularExpressionBuilder
  {
    private const string controlCharacters = @".$^{[(|)*+?\";
    private readonly string _password;
    private string _escapedPassword;
    private readonly WordListProcessOptionsSettings _options;
    private StringBuilder _sb;


    public static string MatchPasswordExpression(string password, WordListProcessOptionsSettings options)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (options == null) throw new ArgumentNullException("options");

      var reb = new RegularExpressionBuilder(password, options);
      var result = reb.MatchPasswordExpression();
      return result;
    }

    private RegularExpressionBuilder(string password, WordListProcessOptionsSettings options)
    {
      _password = password;
      _options = options;
    }

    private string MatchPasswordExpression()
    {
      _escapedPassword = GetEscapedPassword();
      _sb = new StringBuilder();
      AppendStartAnchor();
      AppendEscapedPassword();
      AppendCheckForPasswordWithNumberedSuffix();
      AppendEndAnchor();
      return _sb.ToString();
    }

    private void AppendCheckForPasswordWithNumberedSuffix()
    {
      if (_options.CheckForNumberSuffix)
      {
        AppendOr();
        AppendEscapedPassword();
        AppendNumber();
      }
    }

    private void AppendNumber()
    {
      _sb.Append("[0-9]");
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

    private string GetEscapedPassword()
    {
      var sb = new StringBuilder(_password.Length);
      foreach (var c in _password.Select(Escape))
        sb.Append(c);
      return sb.ToString();
    }

    private static string Escape(char c)
    {
      if (controlCharacters.Any(cc => cc == c))
        return "\\" + c;
      return c.ToString();
    }

    private void AppendEscapedPassword()
    {
      _sb.Append(_escapedPassword);
    }
  }
}