using System;
using Xander.PasswordValidator.Helpers;
namespace Xander.PasswordValidator
{
  public static class CustomWordListFactory
  {
    private static Func<string,string> _mapPath = null;
    internal static CustomWordListRetriever Create()
    {
      var mapPath = _mapPath;
      if (mapPath == null)
        return new CustomWordListRetriever();
      return new CustomWordListRetriever(mapPath);
    }

    public static void Configure(Func<string, string> mapPath)
    {
      _mapPath = mapPath;
    }
  }
}