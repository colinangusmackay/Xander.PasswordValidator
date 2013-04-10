using System.Web.Mvc;
using PasswordValidator.Demo.Web.Models;

namespace PasswordValidator.Demo.Web.Controllers
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
