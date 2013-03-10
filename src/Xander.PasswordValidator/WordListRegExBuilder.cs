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
namespace Xander.PasswordValidator
{
  /// <summary>
  /// A builder that creates a fragment of a regular expression that is used
  /// to validate against a word list.
  /// </summary>
  public abstract class WordListRegExBuilder
  {

    /// <summary>
    /// Builds and returns a fragment of a regular expression.
    /// </summary>
    /// <param name="password">The password being validated</param>
    /// <returns>A fragment of regular expression</returns>
    public abstract string GetRegularExpression(string password);

    /// <summary>
    /// Encodes a string as a regular expression so that the literal string can 
    /// be searched for in the word list.
    /// </summary>
    /// <param name="value">The string to be encoded.</param>
    /// <returns>The encoded string</returns>
    protected string RegExEncode(string value)
    {
      return RegExEncoder.Encode(value);
    }
  }
}