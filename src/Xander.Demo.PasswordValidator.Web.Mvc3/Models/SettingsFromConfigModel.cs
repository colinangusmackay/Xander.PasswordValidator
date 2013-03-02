using Xander.PasswordValidator.Web;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Models
{
  public class SettingsFromConfigModel
  {
    [PasswordValidation]
    public string Password { get; set; } 
  }
}