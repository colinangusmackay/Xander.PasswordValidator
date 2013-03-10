using System.Web.Mvc;
using System.Web.Routing;
using Xander.Demo.PasswordValidator.Web.Mvc3.Helpers;
using Xander.PasswordValidator;
using Xander.PasswordValidator.Web;

namespace Xander.Demo.PasswordValidator.Web.Mvc3
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }

    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          "Default", // Route name
          "{controller}/{action}/{id}", // URL with parameters
          new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
      );
    }

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RegisterPasswordValidation();

      RegisterGlobalFilters(GlobalFilters.Filters);
      RegisterRoutes(RouteTable.Routes);
    }

    private static void RegisterPasswordValidation()
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