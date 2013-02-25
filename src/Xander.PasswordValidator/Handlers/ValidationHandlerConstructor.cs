using System;

namespace Xander.PasswordValidator.Handlers
{
  internal class ValidationHandlerConstructor
  {
    internal ValidationHandlerConstructor(Type handlerType, Func<IPasswordValidationSettings, bool> predicate)
    {
      HandlerType = handlerType;
      Predicate = predicate;
    }

    internal Type HandlerType { get; private set; }
    internal Func<IPasswordValidationSettings, bool> Predicate { get; private set; }

    public ValidationHandler ConstructHandler(IPasswordValidationSettings settings)
    {
      if (NeedsSettings(HandlerType))
        return (ValidationHandler)Activator.CreateInstance(HandlerType, settings);
      return (ValidationHandler)Activator.CreateInstance(HandlerType);

    }

    private static bool NeedsSettings(Type handlerType)
    {
      return handlerType.GetConstructor(new[] { typeof(IPasswordValidationSettings) }) != null;
    }
  }
}