using System.Linq;
using Xander.PasswordValidator.Config;

namespace Xander.PasswordValidator
{
  public class Validator
  {
    private readonly int _minPasswordLength;
    private readonly bool _needsNumber;
    public Validator(int minPasswordLength, bool needsNumber)
    {
      _minPasswordLength = minPasswordLength;
      _needsNumber = needsNumber;
    }

    public Validator()
    {
      var config = PasswordValidationSection.Get();
      _minPasswordLength = config.MinimumPasswordLength;
      _needsNumber = config.NeedsNumber;
    }

    public int MinPasswordLength
    {
      get { return _minPasswordLength; }
    }

    public bool NeedsNumber
    {
      get { return _needsNumber; }
    }

    public ValidationResult Validate(string password)
    {
      if (password.Length < _minPasswordLength)
        return ValidationResult.FailTooShort;

      if ((_needsNumber) && (!password.Any(char.IsDigit)))
        return ValidationResult.FailNumberRequired;

      return ValidationResult.Success;
    }
  }
}