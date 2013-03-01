#region Copyright notice
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

using Xander.PasswordValidator.Config;
using Xander.PasswordValidator.Handlers;

namespace Xander.PasswordValidator
{
  /// <summary>
  /// The Validator class is the main entry point for applications to validate 
  /// a password.
  /// </summary>
  public class Validator : IValidator
  {
    private readonly ValidationHandlerNode _validationChain;

    /// <summary>
    /// Constructs an instance of the `Validator` class using the settings passed 
    /// into the constructor.
    /// </summary>
    /// <param name="settings">The settings that define the rules by which
    /// to validate a password.</param>
    public Validator(IPasswordValidationSettings settings)
    {
      _validationChain = ValidationServiceLocator.GetValidationHandlerChain(settings);
    }

    /// <summary>
    /// Constructs an instance of the `Validator` class using the settings in 
    /// the application's .config file.
    /// </summary>
    public Validator()
      : this(PasswordValidationSection.Get())
    {
    }

    /// <summary>
    /// Checks the given password to ensure that it passes the validation rules
    /// that were given in the constructor.
    /// </summary>
    /// <param name="password">The password to check against the validation rules</param>
    /// <returns>true if the validation has passed and the password is suitable; 
    /// false indicates the password failed the validation.</returns>
    public bool Validate(string password)
    {
      return _validationChain.Validate(password);
    }
  }
}