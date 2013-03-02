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
    private const string CustomFilePath = "TestHelpers\\Files\\";
    private const string FileName = "MyCustomWordList.txt";
    private const string FullCustomFileName = CustomFilePath + FileName;
    private const string FileContents = "NotAValidPassword\nAnotherInvalidPassword";
    
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Retrieve_NullFileName_ThrowsException()
    {
      var retriever = new CustomWordListRetriever();
      retriever.Retrieve(null);
    }

    [Test]
    [ExpectedException(typeof(FileNotFoundException))]
    public void Retrieve_FileDoesntExist_ThrowsException()
    {
      var retriever = new CustomWordListRetriever();
      retriever.Retrieve("I-dont-exist.txt");
    }

    [Test]
    public void Retrieve_FileFound_ReturnsFileContents()
    {
      var retriever = new CustomWordListRetriever();
      string result = retriever.Retrieve(FullCustomFileName);
      Assert.AreEqual(FileContents, result);
    }

    [Test]
    [ExpectedException(typeof(CustomValidationFileException))]
    public void Retrieve_FileError_ThrowsException()
    {
      using (var fs = new FileStream(FullCustomFileName, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        var retriever = new CustomWordListRetriever();
        retriever.Retrieve(FullCustomFileName);
      }
    }

    [Test]
    public void Retrieve_WithMappedPathFileFound_ReturnsFileContents()
    {
      Func<string, string> mapPath = (path) => CustomFilePath + path;
      var retriever = new CustomWordListRetriever(mapPath);
      string result = retriever.Retrieve(FileName);
      Assert.AreEqual(FileContents, result);
    }
  }
}