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
  /// Defines the settings used to configure the password validator.
  /// </summary>
  public interface IPasswordValidationSettings
  {
    /// <summary>
    /// Gets or sets the minimum number of character that a password is permitted to be.
    /// </summary>
    int MinimumPasswordLength { get; set; }

    /// <summary>
    /// Gets or sets whether a password must contain a number.
    /// </summary>
    bool NeedsNumber { get; set; }

    /// <summary>
    /// Gets or sets whether a password must contain a letter.
    /// </summary>
    bool NeedsLetter { get; set; }

    /// <summary>
    /// Gets or sets whether a password must contain a symbol.
    /// </summary>
    /// <remarks>A symbol is defined as any character that is not a letter or a digit.</remarks>
    bool NeedsSymbol { get; set; }

    /// <summary>
    /// Gets a collection of standard word lists that the validator is to check against.
    /// </summary>
    ICollection<StandardWordList> StandardWordLists { get; }

    /// <summary>
    /// Gets a collection of custom word lists that the validator is to check against.
    /// </summary>
    ICollection<string> CustomWordLists { get; }

    /// <summary>
    /// Gets the options with which the word lists are to be processed.
    /// </summary>
    IWordListProcessOptions WordListProcessOptions { get; }

    /// <summary>
    /// Gets a collection of custom validators to use to check the password.
    /// </summary>
    ICollection<Type> CustomValidators { get; }

    /// <summary>
    /// Gets the dictionary that holds the data used to pass to custom validators.
    /// </summary>
    IDictionary<Type, object> CustomSettings { get; } 
  }
}