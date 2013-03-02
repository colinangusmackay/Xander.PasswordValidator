using System;
using NUnit.Framework;
using Xander.PasswordValidator.Web.Exceptions;
namespace Xander.PasswordValidator.Web.TestSuite
{
  [TestFixture]
  public class PasswordValidationSettingsCacheTests
  {
    [SetUp]
    [TearDown]
    public void ResetTestState()
    {
      PasswordValidationSettingsCache.Clear();
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_NullKey_ThrowsException()
    {
      var settings = new PasswordValidationSettings();
      PasswordValidationSettingsCache.Add(null, settings);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Add_NullSettings_ThrowsException()
    {
      PasswordValidationSettingsCache.Add("TheKey", null);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Get_NullKey_ThrowsException()
    {
      PasswordValidationSettingsCache.Get(null);
    }

    [Test]
    public void Get_RetrievesTheSettingsAdded_SettingsAreRetrieved()
    {
      PasswordValidationSettings settings = new PasswordValidationSettings();
      PasswordValidationSettingsCache.Add("myKey", settings);

      var result = PasswordValidationSettingsCache.Get("myKey");
      Assert.AreSame(settings, result);
    }

    [Test]
    [ExpectedException(typeof(PasswordSettingsCacheException))]
    public void Get_WrongKey_ThrowsException()
    {
      PasswordValidationSettingsCache.Get("InvalidKey");
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Remove_NullKey_ThrowsException()
    {
      PasswordValidationSettingsCache.Remove(null);
    }

    [Test]
    [ExpectedException(typeof (PasswordSettingsCacheException))]
    public void Remove_AddedSettingsAreRemoved_ThrowsExceptionWhenRemovedKeyIsSubsequentlyUsed()
    {
      PasswordValidationSettings settings = new PasswordValidationSettings();
      PasswordValidationSettingsCache.Add("EphemeralKey", settings);
      PasswordValidationSettingsCache.Remove("EphemeralKey");
      PasswordValidationSettingsCache.Get("EphemeralKey");
    }
  }
}