using System.Collections.Generic;

namespace Xander.PasswordValidator
{
  public class PasswordValidationSettings : IPasswordValidationSettings
  {
    public PasswordValidationSettings()
    {
      StandardWordLists = new List<StandardWordList>();
    }
    public int MinimumPasswordLength { get; set; }
    public bool NeedsNumber { get; set; }
    public bool NeedsLetter { get; set; }
    public List<StandardWordList> StandardWordLists { get; private set; }

    ICollection<StandardWordList> IPasswordValidationSettings.StandardWordLists
    { get { return StandardWordLists; } }
  }
}