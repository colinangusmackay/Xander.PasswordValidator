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
  public static class RegularExpressionBuilder
  {
    private const string controlCharacters = @".$^{[(|)*+?\";

    public static string MatchPasswordExpression(string password, WordListProcessOptionsSettings options)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (options == null) throw new ArgumentNullException("options");

      string escapedPassword = EscapePassword(password);
      var sb = new StringBuilder();
      AppendStartAnchor(sb);
      sb.Append(escapedPassword);
      AppendCheckForPasswordWithNumberedSuffix(options, sb, escapedPassword);
      AppendEndAnchor(sb);
      return sb.ToString();
    }

    private static void AppendCheckForPasswordWithNumberedSuffix(WordListProcessOptionsSettings options, StringBuilder sb,
                                                                 string escapedPassword)
    {
      if (options.CheckForNumberSuffix)
      {
        AppendOr(sb);
        sb.Append(escapedPassword);
        AppendNumber(sb);
      }
    }

    private static void AppendNumber(StringBuilder sb)
    {
      sb.Append("[0-9]");
    }

    private static void AppendOr(StringBuilder sb)
    {
      sb.Append("|");
    }

    private static void AppendEndAnchor(StringBuilder sb)
    {
      sb.Append("$");
    }

    private static void AppendStartAnchor(StringBuilder sb)
    {
      sb.Append("^");
    }

    private static string EscapePassword(string password)
    {
      StringBuilder sb = new StringBuilder(password.Length);
      AppendEscapedPassword(password, sb);
      return sb.ToString();
    }

    private static void AppendEscapedPassword(string password, StringBuilder sb)
    {
      foreach (var c in password.Select(Escape))
        sb.Append(c);
    }

    private static string Escape(char c)
    {
      if (controlCharacters.Any(cc => cc == c))
        return "\\" + c;
      return c.ToString();
    }

  }
}