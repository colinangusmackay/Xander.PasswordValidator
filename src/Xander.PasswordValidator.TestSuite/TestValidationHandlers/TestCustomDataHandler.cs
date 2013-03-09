namespace Xander.PasswordValidator.TestSuite.TestValidationHandlers
{
  public class TestCustomData
  {
    public bool ShouldPassValidation { get; set; }
  }

  public class TestCustomDataHandler : CustomValidationHandler<TestCustomData>
  {
    public override bool Validate(string password)
    {
      return CustomData.ShouldPassValidation;
    }
  }
}