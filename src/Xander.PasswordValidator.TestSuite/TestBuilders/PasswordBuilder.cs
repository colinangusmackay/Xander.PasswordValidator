
namespace Xander.PasswordValidator.TestSuite.TestBuilders
{
  public class PasswordBuilder : WordListRegExBuilder
  {
    public override string GetRegularExpression(string password)
    {
      return "testpassword1430";
    }
  }
}