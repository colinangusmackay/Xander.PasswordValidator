using System;
using System.Runtime.Serialization;

namespace Xander.PasswordValidator.Exceptions
{
  public class PasswordValidatorConfigException : PasswordValidatorException
  {
    public PasswordValidatorConfigException()
      : base()
    { }

    public PasswordValidatorConfigException(string message)
      : base(message)
    { }

    public PasswordValidatorConfigException(string message, Exception innerException)
      : base(message, innerException)
    { }

    public PasswordValidatorConfigException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    { }
  }
}