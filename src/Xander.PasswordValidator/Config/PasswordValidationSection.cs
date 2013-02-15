using System.Configuration;
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.Config
{
  public class PasswordValidationSection : ConfigurationSection
  {
    private const string MinimumPasswordLengthKey = "minimumPasswordLength";
    private const string SectionNameKey = "passwordValidation/rules";

    public static PasswordValidationSection Get()
    {
      var section = ConfigurationManager.GetSection(SectionNameKey);
      if(section == null)
        throw BuildSectionNotFoundException();
      var result = (PasswordValidationSection) section;
      return result;
    }

    public static void Refresh()
    {
      ConfigurationManager.RefreshSection(SectionNameKey);
    }

    private static PasswordValidatorConfigException BuildSectionNotFoundException()
    {
      string message = string.Format("The configuration file does not contain the section <{0}>", SectionNameKey);
      return new PasswordValidatorConfigException(message);
    }

    [ConfigurationProperty(MinimumPasswordLengthKey, DefaultValue = "8", IsRequired = false)]
    public int MinimumPasswordLength
    {
      get { return (int)this[MinimumPasswordLengthKey]; }
      set { this[MinimumPasswordLengthKey] = value; }
    }
  }
}