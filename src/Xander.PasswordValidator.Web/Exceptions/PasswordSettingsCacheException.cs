#region Copyright Notice
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
using System.Runtime.Serialization;
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.Web.Exceptions
{
  /// <summary>
  /// Represents an error retrieving the settings from the cache.
  /// </summary>
  public class PasswordSettingsCacheException : PasswordValidatorException
  {
    /// <summary>
    /// Initialises the PasswordSettingsCacheException with the given message.
    /// </summary>
    /// <param name="message">The message the exception will have.</param>
    public PasswordSettingsCacheException(string message) 
      : base(message)
    {
    }

    /// <summary>
    /// Initialises a new instance of the PasswordSettingsCacheException class with
    /// a serialised data.
    /// </summary>
    /// <param name="info">The SerializationInfo that holds the serialized 
    /// object data about the exception being thrown. </param>
    /// <param name="context">The StreamingContext that contains contextual
    /// information about the source or destination. </param>
    public PasswordSettingsCacheException(SerializationInfo info, StreamingContext context) 
      : base(info, context)
    {
    }
  }
}