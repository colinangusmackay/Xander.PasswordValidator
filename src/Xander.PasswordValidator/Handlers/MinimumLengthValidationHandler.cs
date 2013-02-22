namespace Xander.PasswordValidator.Handlers
{
  public class MinimumLengthValidationHandler : ValidationHandler
  {
    public MinimumLengthValidationHandler(IPasswordValidationSettings settings) 
      : base(settings)
    {
    }

    protected override bool ValidateImpl(string password)
    {
      return (password.Length >= Settings.MinimumPasswordLength);
    }
  }
}