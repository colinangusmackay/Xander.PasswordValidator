using Xander.PasswordValidator.Web;

namespace PasswordValidator.Demo.Web.Models
{
  public class SettingsFromCodeModel
  {
    [PasswordValidation("NoDates", ErrorMessage = "The password is not acceptable. Passwords cannot look like dates, must have a number and symbol.")]
    public string Password { get; set; } 
  }
}