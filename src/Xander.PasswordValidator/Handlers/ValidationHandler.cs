namespace Xander.PasswordValidator.Handlers
{
  public abstract class ValidationHandler
  {
    protected ValidationHandler(IPasswordValidationSettings settings)
    {
      Settings = settings;
    }
    protected IPasswordValidationSettings Settings { get; private set; }

    public ValidationHandler Successor { get; set; }

    public bool Validate(string password)
    {
      bool hasPassedValidation = ValidateImpl(password);

      if (IsEndOfChain)
        return hasPassedValidation;
      if (hasPassedValidation)
        return Successor.Validate(password);
      return false;
    }

    private bool IsEndOfChain
    {
      get { return Successor == null; }
    }

    protected abstract bool ValidateImpl(string password);
  }
}