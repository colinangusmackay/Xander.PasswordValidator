using System;
using System.Collections.Generic;
using Xander.PasswordValidator.Web.Exceptions;

namespace Xander.PasswordValidator.Web
{
  public static class PasswordValidationSettingsCache
  {
    private static readonly Dictionary<string, IPasswordValidationSettings> _cache = new Dictionary<string, IPasswordValidationSettings>();

    public static void Add(string key, IPasswordValidationSettings settings)
    {
      if (key == null) throw new ArgumentNullException("key");
      if (settings == null) throw new ArgumentNullException("settings");

      _cache[key] = settings;
    }

    public static IPasswordValidationSettings Get(string key)
    {
      if (key == null) throw new ArgumentNullException("key");

      IPasswordValidationSettings result;
      if (_cache.TryGetValue(key, out result))
        return result;
      throw new PasswordSettingsCacheException("The key does not correspond to any stored settings.");
    }

    public static void Remove(string key)
    {
      if (key == null) throw new ArgumentNullException("key");

      _cache.Remove(key);
    }

    public static void Clear()
    {
      _cache.Clear();
    }
  }
}