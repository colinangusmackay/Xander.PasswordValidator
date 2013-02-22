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
      public AlwaysPassesValidationHandler(IPasswordValidationSettings settings) 
        : base(settings)
      {
      }

      protected override bool ValidateImpl(string password)
      {
        log = log + "T";
        return true;
      }
    }

    public class AlwaysFailsValidationHandler : ValidationHandler
    {
      public AlwaysFailsValidationHandler(IPasswordValidationSettings settings) 
        : base(settings)
      {
      }

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
      var handler = new AlwaysFailsValidationHandler(settings);
      handler.Validate("");
      Assert.AreEqual(1, log.Length);
    }

    [Test]
    public void Validate_TwoChainedHandlers_LogLengthIsTwo()
    {
      var handler1 = new AlwaysPassesValidationHandler(settings);
      var handler2 = new AlwaysPassesValidationHandler(settings);
      handler1.Successor = handler2;
      handler1.Validate("");
      Assert.AreEqual(2, log.Length);
    }

    [Test]
    public void Validate_TwoChainedHanldersFirstFails_LogLengthIsOne()
    {
      var handler1 = new AlwaysFailsValidationHandler(settings);
      var handler2 = new AlwaysPassesValidationHandler(settings);
      handler1.Successor = handler2;
      handler1.Validate("");
      Assert.AreEqual(1, log.Length);
      Assert.AreEqual("F", log);
    }

    [Test]
    public void Validate_ThreeChainedHanldersMiddleFails_LogLengthIsTwo()
    {
      var handler1 = new AlwaysPassesValidationHandler(settings);
      var handler2 = new AlwaysFailsValidationHandler(settings);
      var handler3 = new AlwaysPassesValidationHandler(settings);
      handler1.Successor = handler2;
      handler2.Successor = handler3;
      handler1.Validate("");
      Assert.AreEqual(2, log.Length);
      Assert.AreEqual("TF", log);
    }

    [Test]
    public void Validate_ThreeChainedHanldersAllPass_LogLengthIsThree()
    {
      var handler1 = new AlwaysPassesValidationHandler(settings);
      var handler2 = new AlwaysPassesValidationHandler(settings);
      var handler3 = new AlwaysPassesValidationHandler(settings);
      handler1.Successor = handler2;
      handler2.Successor = handler3;
      bool result = handler1.Validate("");
      Assert.AreEqual(3, log.Length);
      Assert.AreEqual("TTT", log);
      Assert.IsTrue(result);
    }

  }
}