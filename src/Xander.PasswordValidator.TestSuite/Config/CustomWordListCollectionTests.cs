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
using NUnit.Framework;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.TestSuite.TestHelpers;
using Xander.PasswordValidator.TestSuite.TestHelpers.Resources;

namespace Xander.PasswordValidator.TestSuite.Config
{
  [TestFixture]
  public class CustomWordListCollectionTests
  {
    private static PasswordValidationSection GetAllWordsPasswordValidationSection()
    {
      string allWordsConfig = ConfigFiles.AllWordsConfig;
      ConfigFileHelper.SetConfigFile(allWordsConfig);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      return config;
    }

    [Test]
    [ExpectedException(typeof(ConfigurationErrorsException), ExpectedMessage = "The configuration is read only.")]
    public void Clear_AllWordsConfigFile_ThrowsReadOnlyExcpetion()
    {
      var config = GetAllWordsPasswordValidationSection();

      Assert.AreNotEqual(0, config.CustomWordLists.Count);
      config.CustomWordLists.Clear();
    }

    [Test]
    public void Clear_SelfBuilt_ResetsToEmpty()
    {
      CustomWordListCollection collection = new CustomWordListCollection();
      Assert.AreEqual(0, collection.Count);
      collection.Add("File1.txt");
      collection.Add("File2.txt");
      Assert.AreEqual(2, collection.Count);
      collection.Clear();
      Assert.AreEqual(0, collection.Count);
    }

    [Test]
    public void IsReadOnly_ConfigFile_IsTrue()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.BasicConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      var collection = (ICollection<string>)config.CustomWordLists;
      Assert.IsTrue(collection.IsReadOnly);
    }

    [Test]
    public void IsReadOnly_SelfBuilt_IsFalse()
    {
      var configCollection = new CustomWordListCollection();
      var collection = (ICollection<string>)configCollection;
      Assert.IsFalse(collection.IsReadOnly);
    }

    [Test]
    public void Remove_SelfBuilt_ItemIsRemoved()
    {
      CustomWordListCollection collection = new CustomWordListCollection();
      collection.Add("file1.txt");
      collection.Add("file2.txt");
      Assert.AreEqual(2, collection.Count);
      bool result = collection.Remove("file1.txt");
      Assert.IsTrue(result);
      Assert.AreEqual(1, collection.Count);
    }

    [Test]
    public void Remove_SelfBuiltRemoveNonExistantItem_RemoveReturnsFalse()
    {
      CustomWordListCollection collection = new CustomWordListCollection();
      collection.Add("file1.txt");
      collection.Add("file2.txt");
      Assert.AreEqual(2, collection.Count);
      bool result = collection.Remove("file3.txt");
      Assert.IsFalse(result);
      Assert.AreEqual(2, collection.Count);
    }

    [Test]
    [ExpectedException(typeof(ConfigurationErrorsException), ExpectedMessage = "The configuration is read only.")]
    public void Remove_AllWordsConfigFile_ThrowsReadOnlyExcpetion()
    {
      var config = GetAllWordsPasswordValidationSection();

      Assert.AreNotEqual(0, config.CustomWordLists.Count);
      config.CustomWordLists.Remove("file3.txt");
    }

    [Test]
    public void CopyTo_AllWordsConfigFile_CopysValuesToSuppliedArray()
    {
      var config = GetAllWordsPasswordValidationSection();
      string[] destination = new string[config.CustomWordLists.Count];
      config.CustomWordLists.CopyTo(destination, 0);
      int i = 0;
      foreach (var item in config.CustomWordLists)
      {
        var element = (CustomWordListItem)item;
        Assert.AreEqual(element.File, destination[i], "i = " + i);
        i++;
      }
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CopyTo_NoArrayPassed_ExceptionThrown()
    {
      var collection = new CustomWordListCollection();
      collection.CopyTo(null, 0);
    }

    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_ArrayIndexIsNegative_ExceptionThrown()
    {
      var collection = new CustomWordListCollection();
      string[] array = new string[1];
      collection.CopyTo(array, -1);
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void CopyTo_DestinationOverflow_ExceptionThrown()
    {
      var collection = new CustomWordListCollection();
      collection.Add("file1.txt");
      collection.Add("file2.txt");
      string[] array = new string[1];
      collection.CopyTo(array, 0);
    }
  }
}