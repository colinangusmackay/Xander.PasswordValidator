/******************************************************************************
 * Copyright (C) 2013 Colin Angus Mackay
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 * 
 ******************************************************************************
 *
 * For more information visit: 
 * https://github.com/colinangusmackay/Xander.PasswordValidator
 * 
 *****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using Xander.PasswordValidator.Web.Exceptions;

namespace Xander.PasswordValidator.Web
{
  [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = false)]
  public class PasswordValidationAttribute : ValidationAttribute
  {
    public IPasswordValidationSettings Settings { get; set; }

    public override bool IsValid(object value)
    {
      CheckValidatorState();

      if (ThereIsNoValue(value) || ValueIsNotAString(value)) 
        return false;

      return IsValidImpl((string) value);
    }

    private bool IsValidImpl(string password)
    {
      var validator = Settings == null ? new Validator() : new Validator(Settings);
      var result = validator.Validate(password);
      return result;
    }

    private static void CheckValidatorState()
    {
      if (!IsInValidState)
      {
        string message = "Password Validation support has not been registered in the web application." +
                         Environment.NewLine +
                         "To add password validation support add the following line to the "+
                         "Application_Start method in your Global.ascx file:" +
                         Environment.NewLine +
                         "PasswordValidatorRegistration.Register()";
        throw new PasswordValidatorRegistrationException(message);
      }
    }

    private static bool ValueIsNotAString(object value)
    {
      return !(value is string);
    }

    private static bool ThereIsNoValue(object value)
    {
      return value == null;
    }

    private static bool IsInValidState
    {
      get { return PasswordValidatorRegistration.IsRegistered; }
    }
  }
}