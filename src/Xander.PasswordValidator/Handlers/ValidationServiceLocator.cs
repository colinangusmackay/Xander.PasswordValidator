namespace Xander.PasswordValidator.Handlers
{
  public static class ValidationServiceLocator
  {
     public static ValidationHandler GetValidationHandler(IPasswordValidationSettings settings)
     {
       ValidationHandler result = new MinimumLengthValidationHandler(settings);

       return result;
     }
  }
}