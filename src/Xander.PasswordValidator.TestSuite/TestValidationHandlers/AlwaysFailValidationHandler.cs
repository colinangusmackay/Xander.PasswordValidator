namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class AlwaysFailValidationHandler : ValidationHandler
  {
    protected override bool ValidateImpl(string password)
    {
      return false;
    }
  }
}