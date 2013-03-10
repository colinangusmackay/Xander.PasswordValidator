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
using System.Configuration;
namespace Xander.PasswordValidator.Config
{
  /// <summary>
  /// Defines the options in the config file for the way that word lists are processed.
  /// </summary>
  public class WordListProcessOptionsElement : ConfigurationElement, IWordListProcessOptions
  {
    private const string CheckForNumberSuffixKey = "checkForNumberSuffix";
    private const string CheckForDoubledUpWordKey = "checkForDoubledUpWord";
    private const string CheckForReversedWordKey = "checkForReversedWord";

    [ConfigurationProperty(CheckForNumberSuffixKey)]
    public bool CheckForNumberSuffix
    {
      get { return (bool)base[CheckForNumberSuffixKey]; }
      set { base[CheckForNumberSuffixKey] = value; }
    }

    [ConfigurationProperty(CheckForDoubledUpWordKey)]
    public bool CheckForDoubledUpWord
    {
      get { return (bool)base[CheckForDoubledUpWordKey]; }
      set { base[CheckForDoubledUpWordKey] = value; }
    }

    [ConfigurationProperty(CheckForReversedWordKey)]
    public bool CheckForReversedWord
    {
      get { return (bool)base[CheckForReversedWordKey]; }
      set { base[CheckForReversedWordKey] = value; }
    }

    ICollection<Type> IWordListProcessOptions.CustomBuilders
    { get { return new Type[0]; } }
  }
}