namespace Xander.PasswordValidator.Handlers
{
  public abstract class SettingsBasedValidationHandler : ValidationHandler
  {
    protected SettingsBasedValidationHandler(IPasswordValidationSettings settings)
    {
      Settings = settings;
    }
    protected IPasswordValidationSettings Settings { get; private set; }
  }
}