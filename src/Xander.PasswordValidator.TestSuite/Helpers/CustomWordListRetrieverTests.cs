using System;
using System.IO;
using NUnit.Framework;
using Xander.PasswordValidator.Exceptions;
using Xander.PasswordValidator.Helpers;

namespace Xander.PasswordValidator.TestSuite.Helpers
{
  [TestFixture]
  public class CustomWordListRetrieverTests
  {
    private const string CustomFileName = "TestHelpers\\Files\\MyCustomWordList.txt";

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
     public void Retrieve_NullFileName_ThrowsException()
     {
       CustomWordListRetriever.Retrieve(null);
     }

    [Test]
    [ExpectedException(typeof(FileNotFoundException))]
    public void Retrieve_FileDoesntExist_ThrowsException()
    {
      CustomWordListRetriever.Retrieve("I-dont-exist.txt");
    }

    [Test]
    public void Retrieve_FileFound_ReturnsFileContents()
    {
      string expected = "NotAValidPassword\nAnotherInvalidPassword";
      string result = CustomWordListRetriever.Retrieve(CustomFileName);
      Assert.AreEqual(expected, result);
      
    }

    [Test]
    [ExpectedException(typeof (CustomValidationFileException))]
    public void Retrieve_FileError_ThrowsException()
    {
      using (FileStream fs = new FileStream(CustomFileName, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        CustomWordListRetriever.Retrieve(CustomFileName);
      }
    }
  }
}