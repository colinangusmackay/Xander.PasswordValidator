using System;
using System.Reflection;

namespace Xander.PasswordValidator.Web.TestSuite.TestHelpers
{
  public static class ValidationRegistrationResetter
  {
    public static void Reset()
    {
      Type t = typeof(PasswordValidatorRegistration);
      var pi = t.GetProperty("IsRegistered", BindingFlags.NonPublic | BindingFlags.Static, null, typeof(bool), new Type[0], null);
      var setter = pi.GetSetMethod(true);
      setter.Invoke(null, new object[] { false });
      CustomWordListFactory.Configure(null);
    }
  }
}
