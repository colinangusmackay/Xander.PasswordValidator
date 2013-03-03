using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Xander.PasswordValidator;
namespace Xander.Demo.PasswordValidator.Web.Mvc3.Helpers
{
  public class PasswordHistoryValidationHandler : ValidationHandler
  {
    public override bool Validate(string password)
    {
      var user = HttpContext.Current.User;
      var history = GetHistory(user);
      return !history.Any(h => string.Compare(password, h, true) == 0);
    }

    private IEnumerable<string> GetHistory(IPrincipal user)
    {
      // Normally this would probably look up details about the user to get
      // the history. But for this example, there are a few canned histories.
      // Also, in a real environment you should never have access to plain text
      // passwords like this. You would have an obfuscated version of the 
      // password, and you would similarly obfuscate the password here and 
      // check the two obfuscated versions to see if they are the same.
      switch (user.Identity.Name)
      {
        case "Eddie":
          return new [] {"MyFirstPassword", "MySecondPassword", "MyThirdPassword"};
        case "Denise":
          return new [] {"FishAndChips", "WithBrownSauce", "AndACanOfCoke"};
        default:
          return new string[0];
      }
    }
}
}