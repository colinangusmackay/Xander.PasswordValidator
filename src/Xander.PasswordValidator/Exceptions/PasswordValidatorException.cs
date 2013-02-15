using System;
using System.Runtime.Serialization;

namespace Xander.PasswordValidator.Exceptions
{
  public class PasswordValidatorException : Exception
  {
     public PasswordValidatorException()
       : base()
     {}

    public PasswordValidatorException(string message)
      : base(message)
    {}

    public PasswordValidatorException(string message, Exception innerException)
      : base(message, innerException)
    {}

    public PasswordValidatorException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {}
  }
}