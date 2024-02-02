using System.ComponentModel;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.Helpers;
using Account.Microservice.Core.Interfaces;
using Account.Microservice.SharedKernel.Interfaces;

namespace Account.Microservice.Core.Services.Settings;
public class SettingService: ISettingService
{
  private readonly IRepository<Setting> _settingRepository;
  public SettingService(IRepository<Setting> settingRepository)
  { 
    _settingRepository = settingRepository;
  }
  /// <summary>
  /// Load settings
  /// </summary>
  /// <typeparam name="T">Type</typeparam>
  /// <param name="companyId">Store identifier for which settings should be loaded</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  public virtual async Task<T> LoadSettingAsync<T>(int companyId = 0) where T : ISettings, new()
  {
    return (T)await LoadSettingAsync(typeof(T), companyId);
  }

  /// <summary>
  /// Load settings
  /// </summary>
  /// <param name="type">Type</param>
  /// <param name="companyId">Store identifier for which settings should be loaded</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  public virtual async Task<ISettings> LoadSettingAsync(Type type, int companyId = 0)
  {
    var settings = Activator.CreateInstance(type);

    foreach (var prop in type.GetProperties())
    {
      // get properties we can read and write to
      if (!prop.CanRead || !prop.CanWrite)
        continue;

      var key = type.Name + "." + prop.Name;
      //load by store
      var setting = await GetSettingByKeyAsync<string>(key: key,"", companyId: companyId, loadSharedValueIfNotFound: true);
      if (setting == null)
        continue;

      if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
        continue;

      if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
        continue;

      var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

      //set property
      prop.SetValue(settings, value, null);
    }

#pragma warning disable CS8603 // Possible null reference return.
    return settings as ISettings;
#pragma warning restore CS8603 // Possible null reference return.
  }

  public virtual async Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue,
           int companyId = 0, bool loadSharedValueIfNotFound = false)
  {
    if (string.IsNullOrEmpty(key))
      return defaultValue;

    var settings = await GetAllSettingsDictionaryAsync();
    key = key.Trim().ToLowerInvariant();
    if (!settings.ContainsKey(key))
      return defaultValue;

    var settingsByKey = settings[key];
    var setting = settingsByKey.FirstOrDefault(x => x.CompanyId == companyId);

    //load shared value?
    if (setting == null && companyId > 0 && loadSharedValueIfNotFound)
      setting = settingsByKey.FirstOrDefault(x => x.CompanyId == 0);

    return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
  }
  public virtual async Task<IList<Setting>> GetAllSettingsAsync()
  {
    //var getAll = new GetAllSetting();
    var settings = await _settingRepository.ListAsync();
    return settings.ToList();
  }
  protected virtual async Task<IDictionary<string, IList<Setting>>> GetAllSettingsDictionaryAsync()
  {
    var settings = await GetAllSettingsAsync();

    var dictionary = new Dictionary<string, IList<Setting>>();
    foreach (var s in settings)
    {
      var resourceName = s.Name.ToLowerInvariant();
      var settingForCaching = new Setting
      {
        Id = s.Id,
        Name = s.Name,
        Value = s.Value,
        CompanyId = s.CompanyId
      };
      if (!dictionary.ContainsKey(resourceName))
        //first setting
        dictionary.Add(resourceName, new List<Setting>
                        {
                            settingForCaching
                        });
      else
        //already added
        //most probably it's the setting with the same name but for some certain store (companyId > 0)
        dictionary[resourceName].Add(settingForCaching);
    }

    return dictionary;
  }
  /// <summary>
  /// Save settings object
  /// </summary>
  /// <typeparam name="T">Type</typeparam>
  /// <param name="companyId">Store identifier</param>
  /// <param name="settings">Setting instance</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  public virtual async Task SaveSettingAsync<T>(T settings, int companyId = 0) where T : ISettings, new()
  {
    /* We do not clear cache after each setting update.
     * This behavior can increase performance because cached settings will not be cleared 
     * and loaded from database after each update */
    foreach (var prop in typeof(T).GetProperties())
    {
      // get properties we can read and write to
      if (!prop.CanRead || !prop.CanWrite)
        continue;

      if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
        continue;

      var key = typeof(T).Name + "." + prop.Name;
      var value = prop.GetValue(settings, null);
      if (value != null)
        await SetSettingAsync(prop.PropertyType, key, value, companyId, false);
      else
        await SetSettingAsync(key, string.Empty, companyId, false);
    }

  }
  /// <summary>
  /// Set setting value
  /// </summary>
  /// <typeparam name="T">Type</typeparam>
  /// <param name="key">Key</param>
  /// <param name="value">Value</param>
  /// <param name="companyId">Store identifier</param>
  /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  public virtual async Task SetSettingAsync<T>(string key, T value, int companyId = 0, bool clearCache = true)
  {
    await SetSettingAsync(typeof(T), key, value!, companyId, clearCache);
  }
  /// <summary>
  /// Set setting value
  /// </summary>
  /// <param name="type">Type</param>
  /// <param name="key">Key</param>
  /// <param name="value">Value</param>
  /// <param name="companyId">Store identifier</param>
  /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  protected virtual async Task SetSettingAsync(Type type, string key, object value, int companyId = 0, bool clearCache = true)
  {
    if (key == null)
      throw new ArgumentNullException(nameof(key));
    key = key.Trim().ToLowerInvariant();
    var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

    var allSettings = await GetAllSettingsDictionaryAsync();
    var settingForCaching = allSettings.ContainsKey(key) ?
        allSettings[key].FirstOrDefault(x => x.CompanyId == companyId) : null;
    if (settingForCaching != null)
    {
      //update
      var setting = await GetSettingByIdAsync(settingForCaching.Id);
      setting.Value = valueStr!;
      await UpdateSettingAsync(setting, clearCache);
    }
    else
    {
      //insert
      var setting = new Setting
      {
        Name = key,
        Value = valueStr!,
        CompanyId = companyId
      };
      await InsertSettingAsync(setting, clearCache);
    }
  }
  /// <summary>
  /// Updates a setting
  /// </summary>
  /// <param name="setting">Setting</param>
  /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  public virtual async Task UpdateSettingAsync(Setting setting, bool clearCache = true)
  {
    if (setting == null)
      throw new ArgumentNullException(nameof(setting));

    await _settingRepository.UpdateAsync(setting);
  }
  /// Adds a setting
  /// </summary>
  /// <param name="setting">Setting</param>
  /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  public virtual async Task InsertSettingAsync(Setting setting, bool clearCache = true)
  {
    await _settingRepository.AddAsync(setting);
  }
  /// <summary>
  /// Gets a setting by identifier
  /// </summary>
  /// <param name="settingId">Setting identifier</param>
  /// <returns>
  /// A task that represents the asynchronous operation
  /// The task result contains the setting
  /// </returns>
  public virtual async Task<Setting> GetSettingByIdAsync(int settingId)
  {
    var setting = await _settingRepository.GetByIdAsync(settingId);
    return setting!;
  }

}
