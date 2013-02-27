namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class AlwaysFailValidationHandler : ValidationHandler
  {
    public override bool Validate(string password)
    {
      return false;
    }
  }
}