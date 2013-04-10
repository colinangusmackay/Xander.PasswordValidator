using Xander.PasswordValidator.Web;

namespace PasswordValidator.Demo.Web.Models
{
  public class PerUserSettingsModel
  {
    [PasswordValidation("History")]
    public string Password { get; set; } 
  }
}