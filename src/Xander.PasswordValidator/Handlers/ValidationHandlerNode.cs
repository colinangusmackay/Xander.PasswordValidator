namespace Xander.PasswordValidator.Handlers
{
  internal class ValidationHandlerNode
  {
    private readonly ValidationHandler _handler;

    public ValidationHandlerNode(ValidationHandler handler)
    {
      _handler = handler;
    }

    public ValidationHandlerNode Successor { get; set; }
    public ValidationHandler Handler { get { return _handler; } }

    public bool Validate(string password)
    {
      bool hasPassedValidation = _handler.Validate(password);

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
    
  }
}