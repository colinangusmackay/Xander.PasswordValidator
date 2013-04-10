using System.Web.Mvc;
using PasswordValidator.Demo.Web.Models;

namespace PasswordValidator.Demo.Web.Controllers
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
