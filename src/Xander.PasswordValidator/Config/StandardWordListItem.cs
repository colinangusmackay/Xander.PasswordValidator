using System.Configuration;

namespace Xander.PasswordValidator.Config
{
  public class StandardWordListItem : ConfigurationElement
  {
    private const string ValueKey = "value";

    [ConfigurationProperty(ValueKey, IsRequired = true, IsKey = true)]
    public StandardWordList Value
    {
      get { return (StandardWordList)base[ValueKey]; }
      set { base[ValueKey] = value; }
    }
  }
}
