using Xander.PasswordValidator.Web;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Models
{
  public class SettingsFromCodeModel
  {
    [PasswordValidation("NoDates")]
    public string Password { get; set; } 
  }
}