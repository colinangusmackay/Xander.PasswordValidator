using System.Linq;

namespace Xander.PasswordValidator.Handlers
{
  public class NeedsSymbolValidationHandler : ValidationHandler
  {
    protected override bool ValidateImpl(string password)
    {
      // Note: char.IsSymbol does not recognise punctuation, quotation marks,
      // parentheses, etc. as symbols. So a symbol is defined as anything that
      // is not a letter or digit.
      return password.Any(c=> !char.IsLetterOrDigit(c));
    }
  }
}