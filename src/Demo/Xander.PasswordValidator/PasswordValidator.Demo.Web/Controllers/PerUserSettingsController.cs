using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Security;
using PasswordValidator.Demo.Web.Models;
using WebMatrix.WebData;

namespace PasswordValidator.Demo.Web.Controllers
{
  public class PerUserSettingsController : Controller
  {
    public ActionResult Index()
    {
      return View("PerUserSettings");
    }

    [HttpPost]
    public ActionResult Index(PerUserSettingsModel model)
    {
      return View("PerUserSettings");
    }

    [HttpPost]
    public ActionResult LogIn(string user)
    {
      //WebSecurity.Login(user, string.Empty, persistCookie: true);
      FormsAuthentication.SetAuthCookie(user, false);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult LogOut()
    {
      FormsAuthentication.SignOut();
      return RedirectToAction("Index");
    }
  }
}
