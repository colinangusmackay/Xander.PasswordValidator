#region copyright notice
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
#endregion

using System;
using System.ComponentModel.DataAnnotations;
using Xander.PasswordValidator.Web.Exceptions;

namespace Xander.PasswordValidator.Web
{
  /// <summary>
  /// Specifies the rules for validating a password in a data field.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = false)]
  public class PasswordValidationAttribute : ValidationAttribute
  {
    /// <summary>
    /// Initialises a new instance of the PasswordValidationAttribute.
    /// </summary>
    public PasswordValidationAttribute()
    {
    }

    /// <summary>
    /// Initialises a new instance of the PasswordValidationAtttibute based on
    /// settings in a cache.
    /// </summary>
    /// <param name="settingsCacheKey">The key to looking up the settings
    /// that contain the rules for this validation.</param>
    public PasswordValidationAttribute(string settingsCacheKey)
    {
      if (settingsCacheKey == null) throw new ArgumentNullException("settingsCacheKey");
      Settings = PasswordValidationSettingsCache.Get(settingsCacheKey);
    }

    internal IPasswordValidationSettings Settings { get; set; }

    /// <summary>
    /// Determines whether the specified value of the object is valid. 
    /// </summary>
    /// <returns>
    /// true if the specified value is valid; otherwise, false.
    /// </returns>
    /// <param name="value">The value of the object to validate. </param>
    /// <exception cref="PasswordValidatorRegistrationException">Thrown if
    /// the password validation support has not been registered in the 
    /// web application. See <see cref="PasswordValidatorRegistration.Register()"/>
    /// for more information.</exception>
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
      if (!IsInAValidState)
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

    private static bool IsInAValidState
    {
      get { return PasswordValidatorRegistration.IsRegistered; }
    }
  }
}