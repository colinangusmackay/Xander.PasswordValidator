namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class AlwaysPassValidationHandler : ValidationHandler
  {
    public override bool Validate(string password)
    {
      return true;
    }
  }
}