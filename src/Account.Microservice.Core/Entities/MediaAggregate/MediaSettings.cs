using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Entities.MediaAggregate;
public class MediaSettings: ISettings
{
  public int MaximumImageSize { get; set; }
  /// <summary>
  /// Maximum allowed support file size
  /// </summary>
  public int SuppportMaxFileSize { get; set; }
  /// <summary>
  /// Maximum allowed manual file size
  /// </summary>
  public int ManualMaxFileSize { get; set; }
  /// <summary>
  /// Maximum allowed logo file size
  /// </summary>
  public int LogoMaxFileSize { get; set; }

  public string PictureSavePath { get; set; } = string.Empty;

  public string PicturePath { get; set; } = string.Empty;

  public string UrlCdn { get; set; } = string.Empty;

  //public static explicit operator MediaSettings(Services.Settings.ISettings v)
  //{
  //  throw new NotImplementedException();
  //}
  /// <summary>
  /// Maximum allowed pamphlet file size
  /// </summary>
}
