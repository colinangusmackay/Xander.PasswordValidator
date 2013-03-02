using NUnit.Framework;
using Xander.PasswordValidator.Helpers;

namespace Xander.PasswordValidator.TestSuite
{
  [TestFixture]
  public class CustomWordListFactoryTests
  {
    [SetUp]
    [TearDown]
    public void ResetTest()
    {
      CustomWordListFactory.Configure(null);
    }

    [Test]
    public void Create_NoMapping_ConstructsWordListRetriever()
    {
      CustomWordListFactory.Configure(null);
      var result = CustomWordListFactory.Create();
      Assert.IsInstanceOf<CustomWordListRetriever>(result);
    }

    [Test]
    public void Create_WithMapping_ConstructsWordListRetriever()
    {
      CustomWordListFactory.Configure((s)=>s);
      var result = CustomWordListFactory.Create();
      Assert.IsInstanceOf<CustomWordListRetriever>(result);
    }
  }
}