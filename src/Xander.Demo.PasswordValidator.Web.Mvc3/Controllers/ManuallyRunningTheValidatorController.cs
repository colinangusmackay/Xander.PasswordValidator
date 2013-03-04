using System.Web.Mvc;
using Xander.Demo.PasswordValidator.Web.Mvc3.Models;
using Xander.PasswordValidator;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Controllers
{
  public class ManuallyRunningTheValidatorController : Controller
  {
    public ActionResult Index()
    {
      return View("ManuallyRunningTheValidator");
    }

    [HttpPost]
    public ActionResult Index(ManuallyRunningTheValidatorModel model)
    {
      var settings = new PasswordValidationSettings();
      settings.MinimumPasswordLength = 6;
      settings.NeedsLetter = true;
      settings.NeedsNumber = true;
      settings.StandardWordLists.Add(StandardWordList.MostCommon500Passwords);
      var validator = new Validator(settings);
      bool result = validator.Validate(model.Password);
      model.IsPasswordAcceptable = result;
      return View("ManuallyRunningTheValidator", model);
    }
  }
}
