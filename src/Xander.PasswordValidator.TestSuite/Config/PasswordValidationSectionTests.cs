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
    [Test]
    public void MinimumPasswordLength_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.MinimumPasswordLength = 10;
      Assert.AreEqual(10, section.MinimumPasswordLength);
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
    public void MinimumPasswordLength_BasicConfigFile_ValueIs12()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.BasicConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(12, config.MinimumPasswordLength);
    }
  }
}