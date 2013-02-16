namespace Xander.PasswordValidator
{
  public class PasswordValidationSettings : IPasswordValidationSettings
  {
    public int MinimumPasswordLength { get; set; }
    public bool NeedsNumber { get; set; }
    public bool NeedsLetter { get; set; }
  }
}