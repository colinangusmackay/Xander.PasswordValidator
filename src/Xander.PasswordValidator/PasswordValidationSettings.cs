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
  public class PasswordValidationSettings : IPasswordValidationSettings
  {
    public PasswordValidationSettings()
    {
      StandardWordLists = new List<StandardWordList>();
      CustomWordLists = new List<string>();
      WordListProcessOptions = new WordListProcessOptions();
      CustomValidators = new List<Type>();
      CustomSettings = new Dictionary<Type, object>();
    }
    public int MinimumPasswordLength { get; set; }
    public bool NeedsNumber { get; set; }
    public bool NeedsLetter { get; set; }
    public bool NeedsSymbol { get; set; }
    public List<StandardWordList> StandardWordLists { get; private set; }
    public List<string> CustomWordLists { get; private set; }
    public List<Type> CustomValidators { get; private set; }
    public Dictionary<Type, object> CustomSettings { get; private set; } 

    public IWordListProcessOptions WordListProcessOptions { get; private set; }

    ICollection<StandardWordList> IPasswordValidationSettings.StandardWordLists
    { get { return StandardWordLists; } }

    ICollection<string> IPasswordValidationSettings.CustomWordLists
    { get { return CustomWordLists; } }

    ICollection<Type> IPasswordValidationSettings.CustomValidators 
    { get { return CustomValidators; } }

    IDictionary<Type, object> IPasswordValidationSettings.CustomSettings { get { return CustomSettings; } } 
  }
}