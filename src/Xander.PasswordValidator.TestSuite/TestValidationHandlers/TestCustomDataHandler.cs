namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class TestCustomData
  {
    public bool ShouldPassValidation { get; set; }
  }

  public class TestCustomDataHandler : CustomValidationHandler<TestCustomData>
  {
    public TestCustomDataHandler(TestCustomData customData)
      : base(customData)
    {
    }

    public override bool Validate(string password)
    {
      return CustomData.ShouldPassValidation;
    }
  }
}