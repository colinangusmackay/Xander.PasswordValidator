using NUnit.Framework;
namespace Xander.PasswordValidator.TestSuite
{
  [TestFixture]
  public class PasswordValidationSettingsTests
  {
    [Test]
    public void StandardWordLists_Constructed_AnEmptyList()
    {
      PasswordValidationSettings settings = new PasswordValidationSettings();
      Assert.AreEqual(0, settings.StandardWordLists.Count);
    }

    [Test]
    public void InterfaceStandardWordLists_Constructed_AnEmptyCollection()
    {
      PasswordValidationSettings settings = new PasswordValidationSettings();
      IPasswordValidationSettings iSettings = settings;
      Assert.AreEqual(0, iSettings.StandardWordLists.Count);
    }
  }
}