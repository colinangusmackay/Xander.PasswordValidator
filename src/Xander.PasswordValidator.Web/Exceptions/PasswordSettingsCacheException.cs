using System;
using System.Runtime.Serialization;
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.Web.Exceptions
{
  public class PasswordSettingsCacheException : PasswordValidatorException
  {
    public PasswordSettingsCacheException(string message) 
      : base(message)
    {
    }

    public PasswordSettingsCacheException(string message, Exception innerException) 
      : base(message, innerException)
    {
    }

    public PasswordSettingsCacheException(SerializationInfo info, StreamingContext context) 
      : base(info, context)
    {
    }
  }
}