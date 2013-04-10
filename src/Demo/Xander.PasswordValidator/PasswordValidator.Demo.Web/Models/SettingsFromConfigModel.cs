using Xander.PasswordValidator.Web;

namespace PasswordValidator.Demo.Web.Models
{
  public class SettingsFromConfigModel
  {
    [PasswordValidation]
    public string Password { get; set; } 
  }
}