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

using System.Linq;

namespace Xander.PasswordValidator.Handlers
{
  internal class ValidationServiceLocator
  {
    private readonly IPasswordValidationSettings _settings;

    private static readonly ValidationHandlerConstructor[] _standardConstructors =
      new[]
        {
          new ValidationHandlerConstructor(typeof (MinimumLengthValidationHandler), s => true),
          new ValidationHandlerConstructor(typeof(NeedsNumberValidationHandler), s=> s.NeedsNumber),
          new ValidationHandlerConstructor(typeof(NeedsLetterValidationHandler), s=>s.NeedsLetter),
          new ValidationHandlerConstructor(typeof(NeedsSymbolValidationHandler), s=>s.NeedsSymbol),
          new ValidationHandlerConstructor(typeof(StandardWordListValidationHandler), s=>s.StandardWordLists.Any()),
          new ValidationHandlerConstructor(typeof(CustomWordListValidationHandler), s=>s.CustomWordLists.Any()) 
        };

    public ValidationServiceLocator(IPasswordValidationSettings settings)
    {
      _settings = settings;
    }

    public static ValidationHandler GetValidationHandler(IPasswordValidationSettings settings)
    {
      var locator = new ValidationServiceLocator(settings);
      var result = locator.GetValidationHandler();
      return result;
    }

    public ValidationHandler GetValidationHandler()
    {
      var customConstructors = _settings.CustomValidators
        .Select(t => new ValidationHandlerConstructor(t, s => true));
      var allConstructors = _standardConstructors
        .Union(customConstructors)
        .Where(c => c.Predicate(_settings));

      ValidationHandler result = null;
      ValidationHandler tail = null;
      foreach (var constructor in allConstructors)
      {
        var current = constructor.ConstructHandler(_settings);
        if (result == null)
          result = current;
        if (tail != null)
          tail.Successor = current;
        tail = current;
      }
      return result;
    }
  }
}