using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Services.Settings;
public interface ISettingService
{
  /// <summary>
  /// Load settings
  /// </summary>
  /// <typeparam name="T">Type</typeparam>
  /// <param name="companyId">Store identifier for which settings should be loaded</param>
  /// <returns>A task that represents the asynchronous operation</returns>
 Task<T> LoadSettingAsync<T>(int companyId = 0) where T : ISettings, new();

  /// <summary>
  /// Load settings
  /// </summary>
  /// <param name="type">Type</param>
  /// <param name="companyId">Store identifier for which settings should be loaded</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  Task<ISettings> LoadSettingAsync(Type type, int companyId = 0);

  /// <summary>
  /// Save settings object
  /// </summary>
  /// <typeparam name="T">Type</typeparam>
  /// <param name="companyId">Store identifier</param>
  /// <param name="settings">Setting instance</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  Task SaveSettingAsync<T>(T settings, int companyId = 0) where T : ISettings, new();
  /// <summary>
  /// Set setting value
  /// </summary>
  /// <typeparam name="T">Type</typeparam>
  /// <param name="key">Key</param>
  /// <param name="value">Value</param>
  /// <param name="storeId">Store identifier</param>
  /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
  /// <returns>A task that represents the asynchronous operation</returns>
  Task SetSettingAsync<T>(string key, T value, int storeId = 0, bool clearCache = true);

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="key"></param>
  /// <param name="defaultValue"></param>
  /// <param name="companyId"></param>
  /// <param name="loadSharedValueIfNotFound"></param>
  /// <returns></returns>
  Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue,
           int companyId = 0, bool loadSharedValueIfNotFound = false);
}
