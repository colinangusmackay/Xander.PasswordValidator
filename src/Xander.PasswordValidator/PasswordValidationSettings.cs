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
  public class PasswordValidationSettings : IPasswordValidationSettings
  {
    /// <summary>
    /// Constructs an instance of the PasswordValidationSettings class
    /// </summary>
    public PasswordValidationSettings()
    {
      StandardWordLists = new List<StandardWordList>();
      CustomWordLists = new List<string>();
      WordListProcessOptions = new WordListProcessOptions();
      CustomValidators = new List<Type>();
      CustomSettings = new Dictionary<Type, object>();
    }

    /// <summary>
    /// Gets or Sets the minimum length a password is permitted to be.
    /// </summary>
    public int MinimumPasswordLength { get; set; }

    /// <summary>
    /// Gets or sets whether a password requires a number in it.
    /// </summary>
    public bool NeedsNumber { get; set; }

    /// <summary>
    /// Gets or sets whether a password requires a letter in it.
    /// </summary>
    public bool NeedsLetter { get; set; }

    /// <summary>
    /// Gets of sets whether a password requires a symbol in it.
    /// </summary>
    /// <remarks>For the purpose of the validator a symbol is any character 
    /// that is not a letter or a number. This is not the same definition
    /// that is used by <see cref="Char.IsSymbol(Char)"/></remarks>
    public bool NeedsSymbol { get; set; }

    /// <summary>
    /// Gets a collection of standard word lists that the validator is to check against.
    /// </summary>
    public List<StandardWordList> StandardWordLists { get; private set; }

    /// <summary>
    /// Gets a collection of custom word lists that the validator is to check against.
    /// </summary>
    public List<string> CustomWordLists { get; private set; }

    /// <summary>
    /// Gets a collection of custom validators to use to check the password.
    /// </summary>
    public List<Type> CustomValidators { get; private set; }

    /// <summary>
    /// Gets the dictionary that holds the data used to pass to custom validators.
    /// </summary>
    public Dictionary<Type, object> CustomSettings { get; private set; }

    /// <summary>
    /// Gets the options with which the word lists are to be processed.
    /// </summary>
    public IWordListProcessOptions WordListProcessOptions { get; private set; }

    ICollection<StandardWordList> IPasswordValidationSettings.StandardWordLists
    { get { return StandardWordLists; } }

    ICollection<string> IPasswordValidationSettings.CustomWordLists
    { get { return CustomWordLists; } }

    ICollection<Type> IPasswordValidationSettings.CustomValidators 
    { get { return CustomValidators; } }

    IDictionary<Type, object> IPasswordValidationSettings.CustomData { get { return CustomSettings; } } 
  }
}