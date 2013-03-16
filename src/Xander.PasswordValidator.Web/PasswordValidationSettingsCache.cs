using System;
using System.Collections.Generic;
using Xander.PasswordValidator.Web.Exceptions;

namespace Xander.PasswordValidator.Web
{
  /// <summary>
  /// A cache of validation rules that can be accessed in the <see cref="PasswordValidationAttribute"/>
  /// </summary>
  public static class PasswordValidationSettingsCache
  {
    private static readonly Dictionary<string, IPasswordValidationSettings> _cache = new Dictionary<string, IPasswordValidationSettings>();

    /// <summary>
    /// Add the specified settings to the cache with the given key.
    /// </summary>
    /// <param name="key">The key with which to reference the settings</param>
    /// <param name="settings">The rules that govern how the password is to be validated.</param>
    /// <exception cref="ArgumentNullException">If key is null</exception>
    /// <exception cref="ArgumentNullException">If settings is null</exception>
    public static void Add(string key, IPasswordValidationSettings settings)
    {
      if (key == null) throw new ArgumentNullException("key");
      if (settings == null) throw new ArgumentNullException("settings");

      _cache[key] = settings;
    }

    /// <summary>
    /// Retrieves the settings for the given key
    /// </summary>
    /// <param name="key">The key under which the settings are stored.</param>
    /// <returns>The settings requested.</returns>
    /// <exception cref="ArgumentNullException">If key is null.</exception>
    /// <exception cref="PasswordSettingsCacheException">If the key does not correspond 
    /// to any stored settings.</exception>
    public static IPasswordValidationSettings Get(string key)
    {
      if (key == null) throw new ArgumentNullException("key");

      IPasswordValidationSettings result;
      if (_cache.TryGetValue(key, out result))
        return result;
      throw new PasswordSettingsCacheException("The key does not correspond to any stored settings.");
    }

    /// <summary>
    /// Removes the settings from the cache with the given key.
    /// </summary>
    /// <param name="key">The key to the settings</param>
    /// <remarks>If they cache does not contain anything with that key then
    /// the cache remains unchanged and no exception is thrown.</remarks>
    /// <exception cref="ArgumentNullException">If key is null.</exception>
    public static void Remove(string key)
    {
      if (key == null) throw new ArgumentNullException("key");

      _cache.Remove(key);
    }

    /// <summary>
    /// Removes all entries from the cache, leaving it empty.
    /// </summary>
    public static void Clear()
    {
      _cache.Clear();
    }
  }
}