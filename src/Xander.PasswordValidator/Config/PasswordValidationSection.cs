using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.Config
{
  public class PasswordValidationSection : ConfigurationSection, IPasswordValidationSettings
  {
    private const string MinimumPasswordLengthKey = "minimumPasswordLength";
    private const string NeedsNumberKey = "needsNumber";
    private const string NeedsLetterKey = "needsLetter";
    private const string StandardWordListsKey = "standardWordLists";
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

    [ConfigurationProperty(NeedsNumberKey, DefaultValue = true, IsRequired = false)]
    public bool NeedsNumber
    {
      get { return (bool) this[NeedsNumberKey]; }
      set { this[NeedsNumberKey] = value; }
    }

    [ConfigurationProperty(NeedsLetterKey, DefaultValue = true, IsRequired = false)]
    public bool NeedsLetter
    {
      get { return (bool) this[NeedsLetterKey]; }
      set { this[NeedsLetterKey] = value; }
    }

    [ConfigurationProperty(StandardWordListsKey, IsRequired = false)]
    public StandardWordListCollection StandardWordLists
    {
      get { return (StandardWordListCollection) base[StandardWordListsKey]; }
      set { base[StandardWordListsKey] = value; }
    }

    ICollection<StandardWordList> IPasswordValidationSettings.StandardWordLists
    {
      get { return StandardWordLists; }
    }
  }
}