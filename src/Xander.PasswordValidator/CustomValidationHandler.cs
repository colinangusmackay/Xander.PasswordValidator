namespace Xander.PasswordValidator
{
  public abstract class CustomValidationHandler<TData> : ValidationHandler
  {
    protected CustomValidationHandler(TData customData)
    {
      CustomData = customData;
    }
    protected TData CustomData { get; private set; }
  }
}