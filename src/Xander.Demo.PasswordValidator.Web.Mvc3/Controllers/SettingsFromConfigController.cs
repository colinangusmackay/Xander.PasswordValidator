using System.Web.Mvc;
using Xander.Demo.PasswordValidator.Web.Mvc3.Models;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Controllers
{
  public class SettingsFromConfigController : Controller
  {
    public ActionResult Index()
    {
      return View("SettingsFromConfig");
    }

    [HttpPost]
    public ActionResult Index(SettingsFromConfigModel model)
    {
      return View("SettingsFromConfig");
    }
  }
}
