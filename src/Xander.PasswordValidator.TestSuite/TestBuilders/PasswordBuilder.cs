using Xander.PasswordValidator.Builders;
namespace Xander.PasswordValidator.TestSuite.TestBuilders
{
  public class PasswordBuilder : WordListRegularExpressionBuilder
  {
    public PasswordBuilder(IWordListProcessOptions options) 
      : base(options)
    {
    }

    public override string GetRegularExpression(string password)
    {
      return "testpassword1430";
    }
  }
}