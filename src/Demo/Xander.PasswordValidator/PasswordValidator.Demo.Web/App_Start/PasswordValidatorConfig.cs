using PasswordValidator.Demo.Web.Helpers;
using Xander.PasswordValidator;
using Xander.PasswordValidator.Web;

namespace PasswordValidator.Demo.Web.App_Start
{
  public class PasswordValidatorConfig
  {
     public static void Register()
     {
       PasswordValidatorRegistration.Register();
       var settings = new PasswordValidationSettings();
       settings.NeedsNumber = true;
       settings.NeedsSymbol = true;
       settings.MinimumPasswordLength = 6;
       settings.StandardWordLists.Add(StandardWordList.FemaleNames);
       settings.StandardWordLists.Add(StandardWordList.MaleNames);
       settings.StandardWordLists.Add(StandardWordList.Surnames);
       settings.StandardWordLists.Add(StandardWordList.MostCommon500Passwords);
       settings.CustomValidators.Add(typeof(NoDatesValidationHandler));
       settings.WordListProcessOptions.CustomBuilders.Add(typeof(NumericPrefixBuilder));
       PasswordValidationSettingsCache.Add("NoDates", settings);

       settings = new PasswordValidationSettings();
       settings.MinimumPasswordLength = 6;
       settings.CustomValidators.Add(typeof(PasswordHistoryValidationHandler));
       settings.CustomSettings.Add(typeof(PasswordHistoryValidationHandler), new Repository());
       PasswordValidationSettingsCache.Add("History", settings);
     }
  }
}