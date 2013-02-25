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

using NUnit.Framework;
using Xander.PasswordValidator.Handlers;
namespace Xander.PasswordValidator.TestSuite.Handlers
{
  [TestFixture]
  public class ValidationHandlerTests
  {
    public static string log;
    public IPasswordValidationSettings settings = new PasswordValidationSettings();
    public class AlwaysPassesValidationHandler : ValidationHandler
    {
      protected override bool ValidateImpl(string password)
      {
        log = log + "T";
        return true;
      }
    }

    public class AlwaysFailsValidationHandler : ValidationHandler
    {
      protected override bool ValidateImpl(string password)
      {
        log = log + "F";
        return false;
      }
    }

    [SetUp]
    public void TestSetUp()
    {
      log = string.Empty;
    }

    [Test]
    public void Validate_SingleHandler_LogLengthIsOne()
    {
      var handler = new AlwaysFailsValidationHandler();
      handler.Validate("");
      Assert.AreEqual(1, log.Length);
    }

    [Test]
    public void Validate_TwoChainedHandlers_LogLengthIsTwo()
    {
      var handler1 = new AlwaysPassesValidationHandler();
      var handler2 = new AlwaysPassesValidationHandler();
      handler1.Successor = handler2;
      handler1.Validate("");
      Assert.AreEqual(2, log.Length);
    }

    [Test]
    public void Validate_TwoChainedHanldersFirstFails_LogLengthIsOne()
    {
      var handler1 = new AlwaysFailsValidationHandler();
      var handler2 = new AlwaysPassesValidationHandler();
      handler1.Successor = handler2;
      handler1.Validate("");
      Assert.AreEqual(1, log.Length);
      Assert.AreEqual("F", log);
    }

    [Test]
    public void Validate_ThreeChainedHanldersMiddleFails_LogLengthIsTwo()
    {
      var handler1 = new AlwaysPassesValidationHandler();
      var handler2 = new AlwaysFailsValidationHandler();
      var handler3 = new AlwaysPassesValidationHandler();
      handler1.Successor = handler2;
      handler2.Successor = handler3;
      handler1.Validate("");
      Assert.AreEqual(2, log.Length);
      Assert.AreEqual("TF", log);
    }

    [Test]
    public void Validate_ThreeChainedHanldersAllPass_LogLengthIsThree()
    {
      var handler1 = new AlwaysPassesValidationHandler();
      var handler2 = new AlwaysPassesValidationHandler();
      var handler3 = new AlwaysPassesValidationHandler();
      handler1.Successor = handler2;
      handler2.Successor = handler3;
      bool result = handler1.Validate("");
      Assert.AreEqual(3, log.Length);
      Assert.AreEqual("TTT", log);
      Assert.IsTrue(result);
    }

  }
}