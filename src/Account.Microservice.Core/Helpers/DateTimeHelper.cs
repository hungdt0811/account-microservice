using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Helpers;
public class DateTimeHelper : IDateTimeHelper
{
  #region Fields

  private readonly DateTimeSettings _dateTimeSettings;

  #endregion

  #region Ctor

  public DateTimeHelper(DateTimeSettings dateTimeSettings)
  {
    _dateTimeSettings = dateTimeSettings;
  }

  #endregion

  #region Utilities

  /// <summary>
  /// Retrieves a System.TimeZoneInfo object from the registry based on its identifier.
  /// </summary>
  /// <param name="id">The time zone identifier, which corresponds to the System.TimeZoneInfo.Id property.</param>
  /// <returns>A System.TimeZoneInfo object whose identifier is the value of the id parameter.</returns>
  protected virtual TimeZoneInfo FindTimeZoneById(string id)
  {
    return TimeZoneInfo.FindSystemTimeZoneById(id);
  }

  #endregion

  #region Methods

  /// <summary>
  /// Returns a sorted collection of all the time zones
  /// </summary>
  /// <returns>A read-only collection of System.TimeZoneInfo objects.</returns>
  public virtual ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
  {
    return TimeZoneInfo.GetSystemTimeZones();
  }

  /// <summary>
  /// Converts the date and time to current user date and time
  /// </summary>
  /// <param name="dt">The date and time (represents local system time or UTC time) to convert.</param>
  /// <returns>
  /// A task that represents the asynchronous operation
  /// The task result contains a DateTime value that represents time that corresponds to the dateTime parameter in customer time zone.
  /// </returns>
  public virtual async Task<DateTime> ConvertToUserTimeAsync(DateTime dt)
  {
    return await ConvertToUserTimeAsync(dt, DateTimeKind.Utc);
  }

  public virtual DateTime ConvertToUserTime(DateTime dt)
  {
    return ConvertToUserTime(dt, DateTimeKind.Utc);
  }

  /// <summary>
  /// Converts the date and time to current user date and time
  /// </summary>
  /// <param name="dt">The date and time (represents local system time or UTC time) to convert.</param>
  /// <param name="sourceDateTimeKind">The source datetimekind</param>
  /// <returns>
  /// A task that represents the asynchronous operation
  /// The task result contains a DateTime value that represents time that corresponds to the dateTime parameter in customer time zone.
  /// </returns>
  public virtual async Task<DateTime> ConvertToUserTimeAsync(DateTime dt, DateTimeKind sourceDateTimeKind)
  {
    dt = DateTime.SpecifyKind(dt, sourceDateTimeKind);
    if (sourceDateTimeKind == DateTimeKind.Local && TimeZoneInfo.Local.IsInvalidTime(dt))
      return dt;

    var currentUserTimeZoneInfo = GetCurrentTimeZone();
    return await Task.FromResult(TimeZoneInfo.ConvertTime(dt, currentUserTimeZoneInfo));
  }

  public virtual DateTime ConvertToUserTime(DateTime dt, DateTimeKind sourceDateTimeKind)
  {
    dt = DateTime.SpecifyKind(dt, sourceDateTimeKind);
    if (sourceDateTimeKind == DateTimeKind.Local && TimeZoneInfo.Local.IsInvalidTime(dt))
      return dt;

    var currentUserTimeZoneInfo = GetCurrentTimeZone();
    return TimeZoneInfo.ConvertTime(dt, currentUserTimeZoneInfo);
  }

  /// <summary>
  /// Converts the date and time to current user date and time
  /// </summary>
  /// <param name="dt">The date and time to convert.</param>
  /// <param name="sourceTimeZone">The time zone of dateTime.</param>
  /// <param name="destinationTimeZone">The time zone to convert dateTime to.</param>
  /// <returns>A DateTime value that represents time that corresponds to the dateTime parameter in customer time zone.</returns>
  public virtual DateTime ConvertToUserTime(DateTime dt, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
  {
    if (sourceTimeZone.IsInvalidTime(dt))
      return dt;

    return TimeZoneInfo.ConvertTime(dt, sourceTimeZone, destinationTimeZone);
  }

  /// <summary>
  /// Converts the date and time to Coordinated Universal Time (UTC)
  /// </summary>
  /// <param name="dt">The date and time (represents local system time or UTC time) to convert.</param>
  /// <returns>A DateTime value that represents the Coordinated Universal Time (UTC) that corresponds to the dateTime parameter. The DateTime value's Kind property is always set to DateTimeKind.Utc.</returns>
  public virtual DateTime ConvertToUtcTime(DateTime dt)
  {
    return ConvertToUtcTime(dt, dt.Kind);
  }

  /// <summary>
  /// Converts the date and time to Coordinated Universal Time (UTC)
  /// </summary>
  /// <param name="dt">The date and time (represents local system time or UTC time) to convert.</param>
  /// <param name="sourceDateTimeKind">The source datetimekind</param>
  /// <returns>A DateTime value that represents the Coordinated Universal Time (UTC) that corresponds to the dateTime parameter. The DateTime value's Kind property is always set to DateTimeKind.Utc.</returns>
  public virtual DateTime ConvertToUtcTime(DateTime dt, DateTimeKind sourceDateTimeKind)
  {
    dt = DateTime.SpecifyKind(dt, sourceDateTimeKind);
    if (sourceDateTimeKind == DateTimeKind.Local && TimeZoneInfo.Local.IsInvalidTime(dt))
      return dt;

    return TimeZoneInfo.ConvertTimeToUtc(dt);
  }

  /// <summary>
  /// Converts the date and time to Coordinated Universal Time (UTC)
  /// </summary>
  /// <param name="dt">The date and time to convert.</param>
  /// <param name="sourceTimeZone">The time zone of dateTime.</param>
  /// <returns>A DateTime value that represents the Coordinated Universal Time (UTC) that corresponds to the dateTime parameter. The DateTime value's Kind property is always set to DateTimeKind.Utc.</returns>
  public virtual DateTime ConvertToUtcTime(DateTime dt, TimeZoneInfo sourceTimeZone)
  {
    if (sourceTimeZone.IsInvalidTime(dt))
    {
      //could not convert
      return dt;
    }

    return TimeZoneInfo.ConvertTimeToUtc(dt, sourceTimeZone);
  }

  /// <summary>
  /// Gets a user time zone
  /// </summary>
  /// <param name="customer">User</param>
  /// <returns>
  /// A task that represents the asynchronous operation
  /// The task result contains the customer time zone; if user is null, then default store time zone
  /// </returns>
  public virtual TimeZoneInfo GetUserTimeZone()
  {
    if (!_dateTimeSettings.AllowUsersToSetTimeZone)
      return DefaultStoreTimeZone;

    TimeZoneInfo? timeZoneInfo = null;

    var timeZoneId = string.Empty;
    //if (user != null)
    //    timeZoneId = await _genericAttributeService.GetAttributeAsync<string>(user, UserDefaults.TimeZoneIdAttribute);

    try
    {
      if (!string.IsNullOrEmpty(timeZoneId))
        timeZoneInfo = FindTimeZoneById(timeZoneId);
    }
    catch (Exception exc)
    {
      Debug.Write(exc.ToString());
    }

    return timeZoneInfo! != null ? timeZoneInfo : DefaultStoreTimeZone;
  }

  /// <summary>
  /// Gets the current user time zone
  /// </summary>
  /// <returns>
  /// A task that represents the asynchronous operation
  /// The task result contains the current user time zone
  /// </returns>
  public virtual TimeZoneInfo GetCurrentTimeZone()
  {
    return GetUserTimeZone();
  }

  /// <summary>
  /// Gets or sets a default store time zone
  /// </summary>
  public virtual TimeZoneInfo DefaultStoreTimeZone
  {
    get
    {
      TimeZoneInfo? timeZoneInfo = null;
      try
      {
        if (!string.IsNullOrEmpty(_dateTimeSettings.DefaultCompanyTimeZoneId))
          timeZoneInfo = FindTimeZoneById(_dateTimeSettings.DefaultCompanyTimeZoneId);
      }
      catch (Exception exc)
      {
        Debug.Write(exc.ToString());
      }

      return timeZoneInfo! != null ? timeZoneInfo: TimeZoneInfo.Local;
    }
  }

  #endregion

  /// <summary>
  /// Format datetime to string
  /// </summary>
  /// <param name="dt"></param>
  /// <returns></returns>
  public string FormatDateTime(DateTime dt)
  {
    return dt.ToString(_dateTimeSettings.FormatDateTime);
  }

  /// <summary>
  /// Format datetime to string
  /// </summary>
  /// <param name="dt"></param>
  /// <returns></returns>
  public string FormatDate(DateTime dt)
  {
    return dt.ToString(_dateTimeSettings.FormatDate);
  }

  /// <summary>
  /// Convert DateTime to string
  /// </summary>
  /// <param name="dt"></param>
  /// <returns></returns>
  public async Task<string> ConvertDateTime(DateTime dt)
  {
    var datetime = await ConvertToUserTimeAsync(dt);
    return FormatDateTime(datetime);
  }

  public string FormatDateTime(DateTime? dt)
  {
    return dt == null ? string.Empty : dt.Value.ToString(_dateTimeSettings.FormatDateTime);
  }

  /// <summary>
  /// Format datetime to string
  /// </summary>
  /// <param name="dt"></param>
  /// <returns></returns>
  public string FormatDate(DateTime? dt)
  {
    return dt == null ? string.Empty : dt.Value.ToString(_dateTimeSettings.FormatDate);
  }

  /// <summary>
  /// Format datetime to string
  /// </summary>
  /// <param name="dt"></param>
  /// <returns></returns>
  public string FormatDateTimeToJp(DateTime? dt)
  {
    return dt == null ? string.Empty : $"{dt.Value.Year}年{dt.Value.Month}月{dt.Value.Day}日発行";
  }

}
