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
  /// <summary>
  /// Represents the validation rules in the config file.
  /// </summary>
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

    /// <summary>
    /// Gets the password validation configuration from the config file.
    /// </summary>
    /// <returns></returns>
    public static PasswordValidationSection Get()
    {
      var section = ConfigurationManager.GetSection(SectionNameKey);
      if(section == null)
        throw BuildSectionNotFoundException();
      var result = (PasswordValidationSection) section;
      return result;
    }

    /// <summary>
    /// Refreshes the password validation rules from the config file.
    /// </summary>
    public static void Refresh()
    {
      ConfigurationManager.RefreshSection(SectionNameKey);
    }

    private static PasswordValidatorConfigException BuildSectionNotFoundException()
    {
      string message = string.Format("The configuration file does not contain the section <{0}>", SectionNameKey);
      return new PasswordValidatorConfigException(message);
    }

    /// <summary>
    /// Gets or sets the minimum number of character that a password is permitted to be.
    /// </summary>
    [ConfigurationProperty(MinimumPasswordLengthKey, DefaultValue = "8", IsRequired = false)]
    public int MinimumPasswordLength
    {
      get { return (int)this[MinimumPasswordLengthKey]; }
      set { this[MinimumPasswordLengthKey] = value; }
    }

    /// <summary>
    /// Gets or sets whether a password must contain a number.
    /// </summary>
    [ConfigurationProperty(NeedsNumberKey, DefaultValue = true, IsRequired = false)]
    public bool NeedsNumber
    {
      get { return (bool) this[NeedsNumberKey]; }
      set { this[NeedsNumberKey] = value; }
    }

    /// <summary>
    /// Gets or sets whether a password must contain a letter.
    /// </summary>
    [ConfigurationProperty(NeedsLetterKey, DefaultValue = true, IsRequired = false)]
    public bool NeedsLetter
    {
      get { return (bool) this[NeedsLetterKey]; }
      set { this[NeedsLetterKey] = value; }
    }

    /// <summary>
    /// Gets or sets whether a password must contain a symbol.
    /// </summary>
    /// <remarks>A symbol is defined as any character that is not a letter or a digit.</remarks>
    [ConfigurationProperty(NeedsSymbolKey, DefaultValue = false, IsRequired = false)]
    public bool NeedsSymbol
    {
      get { return (bool)this[NeedsSymbolKey]; }
      set { this[NeedsSymbolKey] = value; }
    }

    /// <summary>
    /// Gets a collection of <see cref="StandardWordListItem"/> objects that the validator is to 
    /// check against.
    /// </summary>
    [ConfigurationProperty(StandardWordListsKey, IsRequired = false)]
    public StandardWordListCollection StandardWordLists
    {
      get { return (StandardWordListCollection) base[StandardWordListsKey]; }
      set { base[StandardWordListsKey] = value; }
    }

    /// <summary>
    /// Gets a collection of standard word lists that the validator is to check against.
    /// </summary>
    ICollection<StandardWordList> IPasswordValidationSettings.StandardWordLists
    {
      get { return StandardWordLists; }
    }

    /// <summary>
    /// Gets a collection of <see cref="CustomWordListItem"/> objects that the validator is to 
    /// check against.
    /// </summary>
    [ConfigurationProperty(CustomWordListsKey, IsRequired = false)]
    public CustomWordListCollection CustomWordLists
    {
      get { return (CustomWordListCollection)base[CustomWordListsKey]; }
      set { base[CustomWordListsKey] = value; }
    }

    /// <summary>
    /// Gets a collection of custom word lists that the validator is to check against.
    /// </summary>
    ICollection<string> IPasswordValidationSettings.CustomWordLists
    {
      get { return CustomWordLists; }
    }

    /// <summary>
    /// Gets the options with which the word lists are to be processed.
    /// </summary>
    [ConfigurationProperty(WordListProcessOptionsKey)]
    public WordListProcessOptionsElement WordListProcessOptions
    {
      get { return (WordListProcessOptionsElement) base[WordListProcessOptionsKey]; }
      set { base[WordListProcessOptionsKey] = value; }
    }

    /// <summary>
    /// Gets the options with which the word lists are to be processed.
    /// </summary>
    IWordListProcessOptions IPasswordValidationSettings.WordListProcessOptions
    {
      get { return WordListProcessOptions; }
    }

    /// <summary>
    /// Gets a collection of custom validators to use to check the password.
    /// </summary>
    ICollection<Type> IPasswordValidationSettings.CustomValidators
    {
      get { return new Type[0]; }
    }

    /// <summary>
    /// Gets the dictionary that holds the data used to pass to custom validators.
    /// </summary>
    public IDictionary<Type, object> CustomData
    {
      get { return new Dictionary<Type, object>(); }
    }


  }
}