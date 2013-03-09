namespace Xander.Demo.PasswordValidator.Web.Mvc3.Helpers
{
  /// <summary>
  /// This is a dummy repository that would fill in for a 
  /// </summary>
  public class Repository
  {
    public string[] GetPasswordHistory(string userName)
    {
      // Normally this would probably look up details about the user to get
      // the history. But for this example, there are a few canned histories.
      // Also, in a real environment you should never have access to plain text
      // passwords like this. You would have an obfuscated version of the 
      // password, and you would similarly obfuscate the password here and 
      // check the two obfuscated versions to see if they are the same.
      switch (userName)
      {
        case "Eddie":
          return new[] { "MyFirstPassword", "MySecondPassword", "MyThirdPassword" };
        case "Denise":
          return new[] { "FishAndChips", "WithBrownSauce", "AndACanOfCoke" };
        default:
          return new string[0];
      }
    }
  }
}