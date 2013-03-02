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