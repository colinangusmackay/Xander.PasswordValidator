using NUnit.Framework;
using Xander.PasswordValidator.Web.Exceptions;
using Xander.PasswordValidator.Web.Helpers;

namespace Xander.PasswordValidator.Web.TestSuite.Helpers
{
  [TestFixture]
  public class ServerPathMapperTests
  {
    [Test]
    [ExpectedException(typeof(PasswordValidatorVirtualPathMapperException))]
    public void ShouldThrowExceptionBecauseThereIsNoHttpContext()
    {
      ServerPathMapper.MapPath("myWordFile.txt");
    }
  }
}