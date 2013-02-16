using System.Linq;
using Xander.PasswordValidator.Config;

namespace Xander.PasswordValidator
{
  public class Validator
  {
    private readonly int _minPasswordLength;
    private readonly bool _needsNumber;
    private readonly bool _needsLetter;

    public Validator(IPasswordValidationSettings settings)
    {
      _minPasswordLength = settings.MinimumPasswordLength;
      _needsNumber = settings.NeedsNumber;
      _needsLetter = settings.NeedsLetter;
    }

    public Validator()
      : this(PasswordValidationSection.Get())
    {
    }

    public int MinPasswordLength
    {
      get { return _minPasswordLength; }
    }

    public bool NeedsNumber
    {
      get { return _needsNumber; }
    }

    public bool NeedsLetter
    {
      get { return _needsLetter; }
    }

    public ValidationResult Validate(string password)
    {
      if (password.Length < _minPasswordLength)
        return ValidationResult.FailTooShort;

      if ((_needsNumber) && (!password.Any(char.IsDigit)))
        return ValidationResult.FailNumberRequired;

      if ((_needsLetter) && (!password.Any(char.IsLetter)))
        return ValidationResult.FailLetterRequired;

      return ValidationResult.Success;
    }
  }
}