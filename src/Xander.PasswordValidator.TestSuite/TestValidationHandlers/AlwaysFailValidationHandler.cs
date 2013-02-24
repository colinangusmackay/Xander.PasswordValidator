using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class AlwaysFailValidationHandler : ValidationHandler
  {
    public AlwaysFailValidationHandler(IPasswordValidationSettings settings) 
      : base(settings)
    {
    }

    protected override bool ValidateImpl(string password)
    {
      return false;
    }
  }
}