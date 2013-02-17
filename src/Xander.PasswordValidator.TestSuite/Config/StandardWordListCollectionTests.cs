using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.TestSuite.TestHelpers;
using Xander.PasswordValidator.TestSuite.TestHelpers.Resources;

namespace Xander.PasswordValidator.TestSuite.Config
{
  [TestFixture]
  public class StandardWordListCollectionTests
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

      Assert.AreNotEqual(0, config.StandardWordLists.Count);
      config.StandardWordLists.Clear();
    }

    [Test]
    public void Clear_SelfBuilt_ResetsToEmpty()
    {
      StandardWordListCollection collection = new StandardWordListCollection();
      Assert.AreEqual(0, collection.Count);
      collection.Add(StandardWordList.FemaleNames);
      collection.Add(StandardWordList.MaleNames);
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
      var collection = (ICollection<StandardWordList>) config.StandardWordLists;
      Assert.IsTrue(collection.IsReadOnly);
    }

    [Test]
    public void IsReadOnly_SelfBuilt_IsFalse()
    {
      var configCollection = new StandardWordListCollection();
      var collection = (ICollection<StandardWordList>) configCollection;
      Assert.IsFalse(collection.IsReadOnly);
    }

    [Test]
    public void Remove_SelfBuilt_ItemIsRemoved()
    {
      StandardWordListCollection collection = new StandardWordListCollection();
      collection.Add(StandardWordList.FemaleNames);
      collection.Add(StandardWordList.MaleNames);
      Assert.AreEqual(2, collection.Count);
      bool result = collection.Remove(StandardWordList.MaleNames);
      Assert.IsTrue(result);
      Assert.AreEqual(1, collection.Count);
    }

    [Test]
    public void Remove_SelfBuiltRemoveNonExistantItem_RemoveReturnsFalse()
    {
      StandardWordListCollection collection = new StandardWordListCollection();
      collection.Add(StandardWordList.FemaleNames);
      collection.Add(StandardWordList.MaleNames);
      Assert.AreEqual(2, collection.Count);
      bool result = collection.Remove(StandardWordList.Surnames);
      Assert.IsFalse(result);
      Assert.AreEqual(2, collection.Count);
    }

    [Test]
    [ExpectedException(typeof(ConfigurationErrorsException), ExpectedMessage = "The configuration is read only.")]
    public void Remove_AllWordsConfigFile_ThrowsReadOnlyExcpetion()
    {
      var config = GetAllWordsPasswordValidationSection();

      Assert.AreNotEqual(0, config.StandardWordLists.Count);
      config.StandardWordLists.Remove(StandardWordList.MostCommon500Passwords);
    }

    [Test]
    public void CopyTo_AllWordsConfigFile_CopysValuesToSuppliedArray()
    {
      var config = GetAllWordsPasswordValidationSection();
      StandardWordList[] destination = new StandardWordList[config.StandardWordLists.Count];
      config.StandardWordLists.CopyTo(destination, 0);
      int i = 0;
      foreach (var item in config.StandardWordLists)
      {
        var element = (StandardWordListItem) item;
        Assert.AreEqual(element.Value, destination[i], "i = " + i);
        i++;
      }
    }
  }
}