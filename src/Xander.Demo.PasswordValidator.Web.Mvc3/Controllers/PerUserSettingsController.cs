using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Xander.Demo.PasswordValidator.Web.Mvc3.Models;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Controllers
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
