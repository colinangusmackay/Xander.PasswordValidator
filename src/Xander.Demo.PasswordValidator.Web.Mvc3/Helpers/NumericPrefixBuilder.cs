using Xander.PasswordValidator;
namespace Xander.Demo.PasswordValidator.Web.Mvc3.Helpers
{
  public class NumericPrefixBuilder : WordListRegExBuilder
  {
    public override string GetRegularExpression(string password)
    {
      return "[0-9]" + RegExEncode(password);
    }
  }
}