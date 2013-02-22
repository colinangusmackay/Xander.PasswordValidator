using System.Linq;

namespace Xander.PasswordValidator.Handlers
{
  public class NeedsNumberValidationHandler : ValidationHandler
  {
    public NeedsNumberValidationHandler(IPasswordValidationSettings settings)
      : base(settings)
    {
    }

    protected override bool ValidateImpl(string password)
    {
      return password.Any(char.IsDigit);
    }
  }
}