using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace Xander.PasswordValidator.TestSuite.TestHelpers
{
  public class ConfigFileHelper
  {
    public static void SetConfigFile(string content)
    {
      string configPath = GetConfigPath();
      using (var stream = new FileStream(configPath, FileMode.Create, FileAccess.Write, FileShare.Read))
      {
        using (var writer = new StreamWriter(stream, Encoding.UTF8))
        {
          writer.Write(content);
        }
      }
    }

    public static void RemoveConfigFile()
    {
      string configPath = GetConfigPath();
      if (File.Exists(configPath))
        File.Delete(configPath);
    }

    private static string GetConfigPath()
    {
      Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      return config.FilePath;
    }
  }
}