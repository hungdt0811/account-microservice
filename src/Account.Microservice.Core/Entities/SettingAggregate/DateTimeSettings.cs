using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Entities.SettingAggregate;
public class DateTimeSettings: ISettings
{
  /// <summary>
  /// Gets or sets a default time zone identifier
  /// </summary>
  public string? DefaultCompanyTimeZoneId { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether customers are allowed to select theirs time zone
  /// </summary>
  public bool AllowUsersToSetTimeZone { get; set; }
  /// <summary>
  /// 
  /// </summary>
  public string FormatDate { get; set; } = "yyyy/MM/dd";
  /// <summary>
  /// 
  /// </summary>
  public string FormatDateTime { get; set; } = "yyyy/MM/dd HH:mm";
}
