using System.Web.Mvc;
using Xander.Demo.PasswordValidator.Web.Mvc3.Models;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Controllers
{
    public class SettingsFromCodeController : Controller
    {
        public ActionResult Index()
        {
            return View("SettingsFromCode");
        }

        [HttpPost]
        public ActionResult Index(SettingsFromCodeModel model)
        {
          return View("SettingsFromCode");
        }
    }
}
