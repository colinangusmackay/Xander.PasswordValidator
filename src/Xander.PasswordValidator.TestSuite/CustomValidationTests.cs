using NUnit.Framework;

namespace Xander.PasswordValidator.TestSuite
{
  [TestFixture]
  public class CustomValidationTests
  {
    public class TestData
    {
    }

    public class TestCustomValidationHandler : CustomValidationHandler<TestData>
    {
      public TestCustomValidationHandler(TestData customData) : base(customData)
      {
      }

      public override bool Validate(string password)
      {
        Assert.IsNotNull(CustomData);
        return true;
      }
    }

    [Test]
    public void Constructor_CustomValidationHandler_DataObjectPassedBackInProperty()
    {
      TestData data = new TestData();
      TestCustomValidationHandler test = new TestCustomValidationHandler(data);
      test.Validate("SomePassword"); // Asserts are in the overriden Validate
    }
  }
}