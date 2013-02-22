namespace Xander.PasswordValidator.Handlers
{
  public class ValidationServiceLocator
  {
    private readonly IPasswordValidationSettings _settings;
    public ValidationServiceLocator(IPasswordValidationSettings settings)
    {
      _settings = settings;
    }

    public static ValidationHandler GetValidationHandler(IPasswordValidationSettings settings)
    {
      var locator = new ValidationServiceLocator(settings);
      var result = locator.GetValidationHandler();
      return result;
    }

     public ValidationHandler GetValidationHandler()
     {
       ValidationHandler result = GetMinimumLengthHandler();
       GetNeedsNumberHandler(result);


       return result;
     }

     private void GetNeedsNumberHandler(ValidationHandler tail)
     {
       if (!_settings.NeedsNumber)
         return;

       var newTail = new NeedsNumberValidationHandler(_settings);
       tail.Successor = newTail;
     }

    private MinimumLengthValidationHandler GetMinimumLengthHandler()
    {
      return new MinimumLengthValidationHandler(_settings);
    }
  }
}