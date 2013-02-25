namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class AlwaysPassValidationHandler : ValidationHandler
  {
    protected override bool ValidateImpl(string password)
    {
      return true;
    }
  }
}