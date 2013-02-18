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