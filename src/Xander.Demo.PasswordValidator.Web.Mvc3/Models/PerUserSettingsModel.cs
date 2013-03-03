using Xander.PasswordValidator.Web;

namespace Xander.Demo.PasswordValidator.Web.Mvc3.Models
{
  public class PerUserSettingsModel
  {
    [PasswordValidation("History")]
    public string Password { get; set; } 
  }
}