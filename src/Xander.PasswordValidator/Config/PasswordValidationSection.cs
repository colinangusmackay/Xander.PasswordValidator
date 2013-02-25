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
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.Config
{
  public class PasswordValidationSection : ConfigurationSection, IPasswordValidationSettings
  {
    private const string MinimumPasswordLengthKey = "minimumPasswordLength";
    private const string NeedsNumberKey = "needsNumber";
    private const string NeedsLetterKey = "needsLetter";
    private const string NeedsSymbolKey = "needsSymbol";
    private const string StandardWordListsKey = "standardWordLists";
    private const string CustomWordListsKey = "customWordLists";
    private const string WordListProcessOptionsKey = "wordListProcessOptions";
    private const string SectionNameKey = "passwordValidation/rules";

    public static PasswordValidationSection Get()
    {
      var section = ConfigurationManager.GetSection(SectionNameKey);
      if(section == null)
        throw BuildSectionNotFoundException();
      var result = (PasswordValidationSection) section;
      return result;
    }

    public static void Refresh()
    {
      ConfigurationManager.RefreshSection(SectionNameKey);
    }

    private static PasswordValidatorConfigException BuildSectionNotFoundException()
    {
      string message = string.Format("The configuration file does not contain the section <{0}>", SectionNameKey);
      return new PasswordValidatorConfigException(message);
    }

    [ConfigurationProperty(MinimumPasswordLengthKey, DefaultValue = "8", IsRequired = false)]
    public int MinimumPasswordLength
    {
      get { return (int)this[MinimumPasswordLengthKey]; }
      set { this[MinimumPasswordLengthKey] = value; }
    }

    [ConfigurationProperty(NeedsNumberKey, DefaultValue = true, IsRequired = false)]
    public bool NeedsNumber
    {
      get { return (bool) this[NeedsNumberKey]; }
      set { this[NeedsNumberKey] = value; }
    }

    [ConfigurationProperty(NeedsLetterKey, DefaultValue = true, IsRequired = false)]
    public bool NeedsLetter
    {
      get { return (bool) this[NeedsLetterKey]; }
      set { this[NeedsLetterKey] = value; }
    }

    [ConfigurationProperty(NeedsSymbolKey, DefaultValue = false, IsRequired = false)]
    public bool NeedsSymbol
    {
      get { return (bool)this[NeedsSymbolKey]; }
      set { this[NeedsSymbolKey] = value; }
    }

    [ConfigurationProperty(StandardWordListsKey, IsRequired = false)]
    public StandardWordListCollection StandardWordLists
    {
      get { return (StandardWordListCollection) base[StandardWordListsKey]; }
      set { base[StandardWordListsKey] = value; }
    }

    ICollection<StandardWordList> IPasswordValidationSettings.StandardWordLists
    {
      get { return StandardWordLists; }
    }

    [ConfigurationProperty(CustomWordListsKey, IsRequired = false)]
    public CustomWordListCollection CustomWordLists
    {
      get { return (CustomWordListCollection)base[CustomWordListsKey]; }
      set { base[CustomWordListsKey] = value; }
    }

    ICollection<string> IPasswordValidationSettings.CustomWordLists
    {
      get { return CustomWordLists; }
    }

    [ConfigurationProperty(WordListProcessOptionsKey)]
    public WordListProcessOptionsElement WordListProcessOptions
    {
      get { return (WordListProcessOptionsElement) base[WordListProcessOptionsKey]; }
      set { base[WordListProcessOptionsKey] = value; }
    }

    ICollection<Type> IPasswordValidationSettings.CustomValidators
    {
      get { return new Type[0]; }
    }

    public IDictionary<Type, object> CustomSettings
    {
      get { return new Dictionary<Type, object>(); }
    }

    IWordListProcessOptions IPasswordValidationSettings.WordListProcessOptions
    {
      get { return this.WordListProcessOptions; }
    }
  }
}