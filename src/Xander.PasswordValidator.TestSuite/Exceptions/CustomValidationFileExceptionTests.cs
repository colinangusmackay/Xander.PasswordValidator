using NUnit.Framework;
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.TestSuite.Exceptions
{
  [TestFixture]
  public class CustomValidationFileExceptionTests
  {
     [Test]
     public void Constructor_MessageWithFileName_CorrectlyFormatsMessage()
     {
       var ex = new CustomValidationFileException("This is my message", "filename.txt");
       Assert.AreEqual(ex.FileName, "filename.txt");
       Assert.AreEqual(ex.Message, "This is my message\r\nFile Name: filename.txt");
     }
  }
}