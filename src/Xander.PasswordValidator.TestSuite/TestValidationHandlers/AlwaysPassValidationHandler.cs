using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class AlwaysPassValidationHandler : ValidationHandler
  {
    public AlwaysPassValidationHandler(IPasswordValidationSettings settings) 
      : base(settings)
    {
    }

    protected override bool ValidateImpl(string password)
    {
      return true;
    }
  }
}