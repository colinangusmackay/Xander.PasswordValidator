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
    [ExpectedException(ExpectedException = typeof(PasswordValidatorConfigException), ExpectedMessage = "The configuration file does not contain the section <passwordValidation/rules>")]
    public void Get_NoConfigFile_ThrowsException()
    {
      ConfigFileHelper.RemoveConfigFile();
      PasswordValidationSection.Refresh();
      var section = PasswordValidationSection.Get();
    }
    
    [Test]
    public void MinimumPasswordLength_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.MinimumPasswordLength = 10;
      Assert.AreEqual(10, section.MinimumPasswordLength);
    }

    [Test]
    public void MinimumPasswordLength_BasicConfigFile_ValueIs12()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.BasicConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(12, config.MinimumPasswordLength);
    }

    [Test]
    public void NeedsNumber_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.NeedsNumber = true;
      Assert.AreEqual(true, section.NeedsNumber);
      section.NeedsNumber = false;
      Assert.AreEqual(false, section.NeedsNumber);
    }

    [Test]
    public void NeedsNumber_DefaultsOnlyConfigFile_ValueIsTrue()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.DefaultsOnlyConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(true, config.NeedsNumber);
    }

    [Test]
    public void NeedsNumber_BasicConfigFile_ValueIsFalse()
    {
      ConfigFileHelper.SetConfigFile(ConfigFiles.BasicConfigFile);
      PasswordValidationSection.Refresh();
      var config = PasswordValidationSection.Get();
      Assert.AreEqual(false, config.NeedsNumber);
    }

    [Test]
    public void NeedsLetter_RoundtripValue()
    {
      var section = new PasswordValidationSection();
      section.NeedsLetter = true;
      Assert.AreEqual(true, section.NeedsLetter);
      section.NeedsLetter = false;
      Assert.AreEqual(false, section.NeedsLetter);
    }

  }
}