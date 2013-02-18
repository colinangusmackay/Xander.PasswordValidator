using System;
using NUnit.Framework;
using Xander.PasswordValidator.Helpers;
using Xander.PasswordValidator.Resources;
namespace Xander.PasswordValidator.TestSuite.Helpers
{
  [TestFixture]
  public class StandardWordListRetrieverTests
  {
    [Test]
     public void Retrieve_500CommonPasswords_GetsList()
     {
       var list = StandardWordListRetriever.Retrieve(StandardWordList.MostCommon500Passwords);
       var expected = WordLists.MostCommon500Passwords;
       Assert.AreEqual(expected, list);
     }

    [Test]
    public void Retrieve_AllEnumValues_GetsListWithoutIssue()
    {
      var lists = (StandardWordList[])Enum.GetValues(typeof(StandardWordList));
      foreach (var list in lists)
      {
        var currentList = list;
        Assert.DoesNotThrow(()=> StandardWordListRetriever.Retrieve(currentList), 
          "Expected to get a word list for "+currentList);
      }
    }
  }
}