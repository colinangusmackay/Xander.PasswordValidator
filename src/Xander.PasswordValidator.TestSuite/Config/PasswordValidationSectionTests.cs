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

using NUnit.Framework;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.Exceptions;
using Xander.PasswordValidator.TestSuite.TestHelpers;
using Xander.PasswordValidator.TestSuite.TestHelpers.Resources;

namespace Xander.PasswordValidator.TestSuite.Config
{
  [TestFixture]
  public class PasswordValidationSectionTests
  {

    private static PasswordValidationSection GetAllWordsConfig()
    {
      string allWordsConfig = ConfigFiles.AllWordsConfig;
      ConfigFileHelper.SetConfigFile(allWordsConfig);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      return config;
    }
    
    private static PasswordValidationSection GetDefaultOnlyConfig()
    {
      string defaultOnlyConfig = ConfigFiles.DefaultsOnlyConfigFile;
      ConfigFileHelper.SetConfigFile(defaultOnlyConfig);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      return config;
    }

    [SetUp]
    public void SetUp()
    {
      ConfigFileHelper.RemoveConfigFile();
    }

    [TearDown]
    public void TearDown()
    {
      ConfigFileHelper.RemoveConfigFile();      
    }

    [Test]
    [ExpectedException(ExpectedException = typeof(PasswordValidatorConfigException), ExpectedMessage = "The configuration file does not contain the section <passwordValidation/rules>")]
    public void Get_NoConfigFile_ThrowsException()
    {
      ConfigFileHelper.RemoveConfigFile();
      PasswordValidationSection.Refresh();
      var section = PasswordValidationSection.Get();
    }
    
    [Test]
    public void MinimumPasswordLength_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.MinimumPasswordLength = 10;
      Assert.AreEqual(10, section.MinimumPasswordLength);
    }

    [Test]
    public void MinimumPasswordLength_BasicConfigFile_ValueIs12()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.BasicConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(12, config.MinimumPasswordLength);
    }

    [Test]
    public void NeedsNumber_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.NeedsNumber = true;
      Assert.AreEqual(true, section.NeedsNumber);
      section.NeedsNumber = false;
      Assert.AreEqual(false, section.NeedsNumber);
    }

    [Test]
    public void NeedsNumber_DefaultsOnlyConfigFile_ValueIsTrue()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(true, config.NeedsNumber);
    }

    [Test]
    public void NeedsNumber_BasicConfigFile_ValueIsFalse()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.BasicConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(false, config.NeedsNumber);
    }

    [Test]
    public void NeedsLetter_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.NeedsLetter = true;
      Assert.AreEqual(true, section.NeedsLetter);
      section.NeedsLetter = false;
      Assert.AreEqual(false, section.NeedsLetter);
    }

    [Test]
    public void StandardWordLists_RoundTripValue()
    {
      var section = new PasswordValidationSection();
      var collection = new StandardWordListCollection();
      Assert.AreNotSame(collection, section.StandardWordLists);
      section.StandardWordLists = collection;
      Assert.AreSame(collection, section.StandardWordLists);
    }

    [Test]
    public void InterfaceStandardWordLists_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      IPasswordValidationSettings iSection = section;
      var collection = new StandardWordListCollection();
      Assert.AreNotSame(collection, iSection.StandardWordLists);
      section.StandardWordLists = collection;
      Assert.AreSame(collection, iSection.StandardWordLists);
    }

    [Test]
    public void StandardWordLists_AllWordsConfig_FullComplimentOfWordLists()
    {
      var config = GetAllWordsConfig();
      StandardWordListCollection standardWordListCollection = config.StandardWordLists;
      Assert.AreEqual(4, standardWordListCollection.Count);

      Assert.IsTrue(standardWordListCollection.Contains(StandardWordList.FemaleNames));
      Assert.IsTrue(standardWordListCollection.Contains(StandardWordList.MaleNames)); 
      Assert.IsTrue(standardWordListCollection.Contains(StandardWordList.MostCommon500Passwords));
      Assert.IsTrue(standardWordListCollection.Contains(StandardWordList.Surnames));
    }


    [Test]
    public void CustomWordLists_RoundTripValue()
    {
      var section = new PasswordValidationSection();
      var collection = new CustomWordListCollection();
      Assert.AreNotSame(collection, section.CustomWordLists);
      section.CustomWordLists = collection;
      Assert.AreSame(collection, section.CustomWordLists);
    }

    [Test]
    public void InterfaceCustomLists_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      IPasswordValidationSettings iSection = section;
      var collection = new CustomWordListCollection();
      Assert.AreNotSame(collection, iSection.CustomWordLists);
      section.CustomWordLists = collection;
      Assert.AreSame(collection, iSection.CustomWordLists);
    }


    [Test]
    public void WordListProcessOptions_RoundtripValue()
    {
      var value = new WordListProcessOptionsElement();
      var section = new PasswordValidationSection();
      section.WordListProcessOptions = value;

      // Through the regular getter
      Assert.AreSame(value, section.WordListProcessOptions);

      // Through the expicit interface getter
      var options = ((IPasswordValidationSettings) section).WordListProcessOptions;
      Assert.AreSame(value, options);
    }

    [Test]
    public void WordListProcessOptions_DefaultsOnlyConfig_CheckForNumberSuffixDefaultOption()
    {
      var config = GetDefaultOnlyConfig();

      Assert.IsNotNull(config.WordListProcessOptions);

      Assert.IsFalse(config.WordListProcessOptions.CheckForNumberSuffix);
    }

    [Test]
    public void CheckForNumberSuffix_AllWordsConfig_CheckForNumberSuffixIsTrue()
    {
      var config = GetAllWordsConfig();
      Assert.IsTrue(config.WordListProcessOptions.CheckForNumberSuffix);
    }

    [Test]
    public void WordListProcessOptions_DefaultOnlyConfig_CheckForDoubledUpWordDefaultOption()
    {
      var config = GetDefaultOnlyConfig();
      Assert.IsFalse(config.WordListProcessOptions.CheckForDoubledUpWord);
    }

    [Test]
    public void WordListProcessOptions_AllWordsConfig_CheckForDoubledUpWordIsTrue()
    {
      var config = GetAllWordsConfig();
      Assert.IsTrue(config.WordListProcessOptions.CheckForDoubledUpWord);
    }
  }
}