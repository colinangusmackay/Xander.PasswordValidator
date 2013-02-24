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

using Xander.PasswordValidator.Helpers;

namespace Xander.PasswordValidator.Builders
{
  public class DoubledUpWordExpressionBuilder : WordListRegularExpressionBuilder
  {
    public DoubledUpWordExpressionBuilder(IWordListProcessOptions options) 
      : base(options)
    {
    }

    public override string GetRegularExpression(string password)
    {
      if ((Options.CheckForDoubledUpWord) && (IsPasswordDoubledUp(password)))
        return GetRegularExpressionFragment(password);
      return string.Empty;
    }

    private string GetRegularExpressionFragment(string password)
    {
      string firstHalf = GetFirstHalfOfPassword(password);
      string escapedHalf = RegularExpressionEncoder.Encode(firstHalf);
      return escapedHalf;
    }

    private bool IsPasswordDoubledUp(string password)
    {
      if (password.Length % 2 == 1)
        return false;
      string firstHalf = GetFirstHalfOfPassword(password);
      return (password.EndsWith(firstHalf));
    }

    private string GetFirstHalfOfPassword(string password)
    {
      return password.Substring(0, password.Length / 2);
    }
  }
}