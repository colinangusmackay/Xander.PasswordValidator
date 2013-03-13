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

namespace Xander.PasswordValidator
{
  /// <summary>
  /// Represents the options used when processing word lists.
  /// </summary>
  public class WordListProcessOptions : IWordListProcessOptions
  {
    /// <summary>
    /// Instantiates the new WordLisrtProcessOptions class.
    /// </summary>
    public WordListProcessOptions()
    {
      CustomBuilders = new List<Type>();
    }

    /// <summary>
    /// Indicates whether the password should be checked to see if it is a
    /// word in the list with a digit appended to it.
    /// </summary>
    public bool CheckForNumberSuffix { get; set; }

    /// <summary>
    /// Indicates whether the password should be checked to see if it is a
    /// word in the list repeated.
    /// </summary>
    public bool CheckForDoubledUpWord { get; set; }

    /// <summary>
    /// Indicates whether the password should be checked to see if it is a
    /// word in the list in reverse.
    /// </summary>
    public bool CheckForReversedWord { get; set; }

    /// <summary>
    /// Gets a collection of types of custom regular expression builders.
    /// </summary>
    /// <seealso cref="WordListRegExBuilder"/>
    public List<Type> CustomBuilders { get; private set; }

    ICollection<Type> IWordListProcessOptions.CustomBuilders
    { get { return CustomBuilders; } }
  }
}