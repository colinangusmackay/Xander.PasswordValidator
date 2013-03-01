using System;
using System.ComponentModel.DataAnnotations;

namespace Xander.PasswordValidator.Web
{
  [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = false)]
  public class PasswordValidationAttribute : ValidationAttribute
  {
    public IPasswordValidationSettings Settings { get; set; }

    public override bool IsValid(object value)
    {
      var password = (string) value;
      var validator = new Validator(Settings);
      var result = validator.Validate(password);
      return result;
    }

  }
}