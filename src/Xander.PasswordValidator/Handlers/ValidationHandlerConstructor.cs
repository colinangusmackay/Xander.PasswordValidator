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
      if (NeedsSettings)
        return CreateSettingsBasedHandler(settings);
      if (IsCustomHandler)
        return CreateCustomHandler(settings);
      return CreateBasicHandler();
    }

    private ValidationHandler CreateCustomHandler(IPasswordValidationSettings settings)
    {
      object customData = GetCustomData(settings);
      var result = (ValidationHandler) Activator.CreateInstance(HandlerType);
      result.SetData(customData);
      return result;
    }

    private ValidationHandler CreateBasicHandler()
    {
      return (ValidationHandler)Activator.CreateInstance(HandlerType);
    }

    private ValidationHandler CreateSettingsBasedHandler(IPasswordValidationSettings settings)
    {
      return (ValidationHandler)Activator.CreateInstance(HandlerType, settings);
    }

    private object GetCustomData(IPasswordValidationSettings settings)
    {
      var allCustomData = settings.CustomData;
      object result;
      if (allCustomData.TryGetValue(HandlerType, out result))
        return result;
      return null;
    }

    private bool IsCustomHandler
    {
      get
      {
        // Type.IsSubclassOf does't work for generic types.
        // http://www.pvladov.com/2012/05/get-all-derived-types-of-class.html
        var baseType = typeof (CustomValidationHandler<>).GetGenericTypeDefinition();
        var type = HandlerType;
        while (type != null)
        {
          var currentType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
          if (currentType == baseType)
            return true;
          type = type.BaseType;
        }
        return false;
      }
    }

    private bool NeedsSettings
    {
      get
      {
        return HandlerType.GetConstructor(new[] { typeof(IPasswordValidationSettings) }) != null;
      }
    }
  }
}