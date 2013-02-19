using System;
using System.Runtime.Serialization;

namespace Xander.PasswordValidator.Exceptions
{
  public class CustomValidationFileException : PasswordValidatorException
  {
    public CustomValidationFileException(string message, string fileName) 
      : base(FullMessage(message, fileName))
    {
      FileName = fileName;
    }

    public CustomValidationFileException(string message, string fileName, Exception innerException) 
      : base(FullMessage(message, fileName), innerException)
    {
    }

    public CustomValidationFileException(SerializationInfo info, StreamingContext context) 
      : base(info, context)
    {
    }

    public string FileName { get; private set; }

    private static string FullMessage(string message , string fileName )
    {
      return string.Format("{0}\r\nFile Name: {1}", message, fileName);
    }
  }
}