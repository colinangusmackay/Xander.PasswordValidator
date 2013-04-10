using System;
using Xander.PasswordValidator;

namespace PasswordValidator.Demo.Web.Helpers
{
  /// <summary>
  /// Does not permit passwords that look like dates.
  /// </summary>
  public class NoDatesValidationHandler : ValidationHandler
  {
    public override bool Validate(string password)
    {
      DateTime date;
      var parseResult = DateTime.TryParse(password, out date);
      return !parseResult;
    }
  }
}