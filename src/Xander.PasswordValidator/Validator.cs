using Xander.PasswordValidator.Config;

namespace Xander.PasswordValidator
{
  public class Validator
  {
    private readonly int _minPasswordLength;
    public Validator(int minPasswordLength)
    {
      _minPasswordLength = minPasswordLength;
    }

    public Validator()
    {
      var config = PasswordValidationSection.Get();
      _minPasswordLength = config.MinimumPasswordLength;
    }

    public int MinPasswordLength
    {
      get { return _minPasswordLength; }
    }

    public bool Validate(string password)
    {
      if (password.Length >= _minPasswordLength)
        return true;
      return false;
    }
  }
}