using Xander.PasswordValidator;

namespace PasswordValidator.Demo.Web.Helpers
{
  public class NumericPrefixBuilder : WordListRegExBuilder
  {
    public override string GetRegularExpression(string password)
    {
      return "[0-9]" + RegExEncode(password);
    }
  }
}