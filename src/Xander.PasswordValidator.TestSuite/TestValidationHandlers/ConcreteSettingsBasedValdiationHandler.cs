using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  internal class ConcreteSettingsBasedValdiationHandler : SettingsBasedValidationHandler
  {
    public ConcreteSettingsBasedValdiationHandler(IPasswordValidationSettings settings) 
      : base(settings)
    {
    }

    public override bool Validate(string password)
    {
      return true;
    }
  }
}