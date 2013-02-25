using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class ConcreteSettingsBasedValdiationHandler : SettingsBasedValidationHandler
  {
    public ConcreteSettingsBasedValdiationHandler(IPasswordValidationSettings settings) 
      : base(settings)
    {
    }

    protected override bool ValidateImpl(string password)
    {
      return true;
    }
  }
}