using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xander.PasswordValidator.Resources;

namespace Xander.PasswordValidator.Helpers
{
  public class StandardWordListRetriever
  {
    public static string Retrieve(StandardWordList standardWordList)
    {
      var resourceManager = WordLists.ResourceManager;
      var resourceName = standardWordList.ToString();
      var result = resourceManager.GetString(resourceName);
      return result;
    }
  }
}
