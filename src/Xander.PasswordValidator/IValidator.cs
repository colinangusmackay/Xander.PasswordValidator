namespace Xander.PasswordValidator
{
  public interface IValidator
  {
    bool Validate(string password);
  }
}